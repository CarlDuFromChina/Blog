<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :columns="columns"
    :operations="operations"
    :editComponent="editComponent"
  ></sp-list>
</template>

<script>
import sysParamGroupEdit from './sysParamGroupEdit';

export default {
  name: 'sysParamGroupList',
  data() {
    return {
      controllerName: 'sys_param_group',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' },
        { prop: 'updated_by_name', label: '最后修改人' },
        { prop: 'updated_at', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysParamGroupEdit
    };
  }
};
</script>

<style></style>
