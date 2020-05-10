<template>
  <el-form ref="form" :model="data" label-width="80px">
    <el-row>
      <el-col :span="12">
        <el-form-item label="菜单名称">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="路由">
          <el-input v-model="data.router"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="上级菜单">
          <el-select v-model="data.parentid" placeholder="请选择上级菜单">
            <el-option v-for="(item, index) in selectData" :key="index" :label="item.name" :value="item.Id"></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="12">
        <el-form-item label="状态">
          <el-select v-model="data.stateCode" placeholder="请选择状态">
            <el-option
              v-for="(item, index) in [
                { name: '启用', value: 1 },
                { name: '禁用', value: 0 }
              ]"
              :key="index"
              :label="item.name"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col style="text-align:right">
        <span class="dialog-footer">
          <el-button @click="$emit('close')">取 消</el-button>
          <el-button type="primary" @click="confirm">确 定</el-button>
        </span>
      </el-col>
    </el-row>
  </el-form>
</template>

<script>
export default {
  name: 'sysmenu-edit',
  props: {
    relatedAttr: {
      type: Object
    }
  },
  data() {
    return {
      controllerName: 'sysmenu',
      Id: '',
      dialogVisible: false,
      data: {},
      selectData: []
    };
  },
  created() {
    if (this.relatedAttr && this.relatedAttr.id) {
      this.Id = this.relatedAttr.id;
      sp.get(`api/${this.controllerName}/GetData?id=${this.Id}`).then(resp => {
        this.data = resp;
      });
    }
    this.getSelectData();
  },
  watch: {
    'data.stateCode': {
      handler() {
        this.data.stateCodeName = this.data.stateCode === 1 ? '启用' : '禁用';
      }
    }
  },
  methods: {
    getSelectData() {
      sp.get(`api/${this.controllerName}/GetFirstMenu`).then(resp => {
        this.selectData = resp;
      });
    },
    confirm() {
      const operateName = sp.isNullOrEmpty(this.Id) ? 'CreateData' : 'UpdateData';
      if (sp.isNullOrEmpty(this.Id)) {
        this.data.Id = sp.newUUID();
      }
      sp.post(`api/${this.controllerName}/${operateName}`, this.data).then(() => {
        this.$emit('close');
        this.$emit('load-data');
        this.$message.success('添加成功');
      });
    }
  }
};
</script>

<style></style>
