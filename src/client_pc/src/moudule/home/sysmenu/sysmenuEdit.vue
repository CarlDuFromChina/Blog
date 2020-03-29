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
        this.$message.success('添加成功');
      });
    }
  }
};
</script>

<style>

</style>
