<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import sysConfigEdit from './sysConfigEdit';

export default {
  name: 'sysConfigList',
  data() {
    return {
      controllerName: 'SysConfig',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'value', label: '值' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' },
        { prop: 'modified_by_name', label: '最后修改人' },
        { prop: 'updated_at', label: '最后修改日期', type: 'datetime' }
      ],
      editComponent: sysConfigEdit
    };
  }
};
</script>

<style></style>
