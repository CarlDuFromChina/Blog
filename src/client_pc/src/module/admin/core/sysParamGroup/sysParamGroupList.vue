<template>
  <sp-list :controllerName="controllerName" :columns="columns" :operations="operations" :editComponent="editComponent"></sp-list>
</template>

<script>
import sysParamGroupEdit from './sysParamGroupEdit';

export default {
  name: 'sysParamGroupList',
  data() {
    return {
      controllerName: 'SysParamGroup',
      operations: ['new', 'delete', 'search'],
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
  }
};
</script>

<style></style>
