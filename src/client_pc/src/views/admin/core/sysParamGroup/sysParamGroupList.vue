<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :columns="columns"
    :operations="operations"
    :editComponent="editComponent"
    :exportData="exportData"
  ></sp-list>
</template>

<script>
import sysParamGroupEdit from './sysParamGroupEdit';

export default {
  name: 'sysParamGroupList',
  data() {
    return {
      controllerName: 'SysParamGroup',
      operations: ['new', 'delete', 'export', 'search'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'modifiedByName', label: '最后修改人' },
        { prop: 'modifiedOn', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysParamGroupEdit
    };
  },
  methods: {
    exportData() {
      const selectionIds = this.$refs.list.selectionIds;
      if (!selectionIds || selectionIds.length === 0) {
        this.$message.warning('请选择一项，再进行导出');
        return;
      }
      if (selectionIds.length > 1) {
        this.$message.warning('最多导出一条记录');
      }

      const id = selectionIds[0];
      sp.get(`api/${this.controllerName}/Export?id=${id}`);
    }
  }
};
</script>

<style></style>
