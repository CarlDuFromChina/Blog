<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent" :allow-select="true"></sp-list>
</template>

<script>
import sysEntityEdit from './sysEntityEdit';

export default {
  name: 'sysEntityList',
  data() {
    return {
      controllerName: 'SysEntity',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'modifiedByName', label: '最后修改人' },
        { prop: 'modifiedOn', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysEntityEdit
    };
  }
};
</script>

<style></style>
