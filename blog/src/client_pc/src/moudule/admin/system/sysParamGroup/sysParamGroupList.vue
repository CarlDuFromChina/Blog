<template>
  <sp-list :controllerName="controllerName" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import sysParamEdit from './sysParamEdit';

export default {
  name: 'sysParamGroupList',
  data() {
    return {
      controllerName: 'SysParamGroup',
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'modifiedByName', label: '最后修改人' },
        { prop: 'modifiedOn', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysParamEdit
    };
  }
};
</script>

<style></style>
