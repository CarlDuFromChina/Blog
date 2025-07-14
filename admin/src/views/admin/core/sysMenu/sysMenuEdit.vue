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
          <a-input-number v-model="data.menu_index" style="width:100%"></a-input-number>
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
          <a-switch v-model="data.statecode"></a-switch>
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
      controllerName: 'sys_menu',
      id: '',
      dialogVisible: false,
      data: {},
      selectData: []
    };
  },
  created() {
    if (this.relatedAttr && this.relatedAttr.id) {
      this.id = this.relatedAttr.id;
      sp.get(`api/${this.controllerName}/${this.id}`).then(resp => {
        this.data = resp;
      });
    }
    this.getSelectData();
  },
  methods: {
    handleParentIdChange(val) {
      const obj = this.selectData.find(item => item.id === val);
      if (!sp.isNull(obj)) {
        this.data.parentid_name = obj.name;
      }
    },
    getSelectData() {
      sp.get(`api/${this.controllerName}/first_menu`).then(resp => {
        this.selectData = resp;
      });
    },
    async saveData() {
      const operateName = sp.isNullOrEmpty(this.id) ? 'CreateData' : 'UpdateData';
      if (sp.isNullOrEmpty(this.id)) {
        await sp.post(`api/${this.controllerName}`, this.data);
      } else {
        await sp.put(`api/${this.controllerName}`, this.data);
      }
      this.$emit('close');
      this.$emit('load-data');
      this.$message.success(operateName === 'CreateData' ? '添加成功' : '更新成功');
    }
  }
};
</script>
