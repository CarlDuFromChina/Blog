<template>
  <a-form-model ref="form" :model="data">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="菜单名称">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="路由">
          <a-input v-model="data.router"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="索引">
          <a-input-number v-model="data.menu_Index" style="width:100%"></a-input-number>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="上级菜单">
          <a-select v-model="data.parentid" placeholder="请选择上级菜单" @change="handleParentIdChange">
            <a-select-option v-for="(item, index) in selectData" :key="index" :value="item.id">{{ item.name }}</a-select-option>
          </a-select>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="图标">
          <a-input v-model="data.icon"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="状态">
          <a-radio-group v-model="data.stateCode" @change="handleStateCodeChange">
            <a-radio v-for="(item, index) in [{ name: '启用', value: 1 }, { name: '禁用', value: 0 }]" :key="index" :value="item.value">{{
              item.name
            }}</a-radio>
          </a-radio-group>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
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
      id: '',
      dialogVisible: false,
      data: {},
      selectData: []
    };
  },
  created() {
    if (this.relatedAttr && this.relatedAttr.id) {
      this.id = this.relatedAttr.id;
      sp.get(`api/${this.controllerName}/GetData?id=${this.id}`).then(resp => {
        this.data = resp;
      });
    }
    this.getSelectData();
  },
  methods: {
    handleParentIdChange(val) {
      const obj = this.selectData.find(item => item.id === val);
      if (!sp.isNull(obj)) {
        this.data.parentIdName = obj.name;
      }
    },
    handleStateCodeChange() {
      this.data.stateCode_name = this.data.stateCode === 1 ? '启用' : '禁用';
    },
    getSelectData() {
      sp.get(`api/${this.controllerName}/GetFirstMenu`).then(resp => {
        this.selectData = resp;
      });
    },
    saveData() {
      const operateName = sp.isNullOrEmpty(this.id) ? 'CreateData' : 'UpdateData';
      if (sp.isNullOrEmpty(this.id)) {
        this.data.id = uuid.generate();
      }
      sp.post(`api/${this.controllerName}/${operateName}`, this.data).then(() => {
        this.$emit('close');
        this.$emit('load-data');
        this.$message.success(operateName === 'CreateData' ? '添加成功' : '更新成功');
      });
    }
  }
};
</script>
