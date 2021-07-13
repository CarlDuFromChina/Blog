<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import roleEdit from './roleEdit';

export default {
  name: 'sysRoleList',
  data() {
    return {
      controllerName: 'SysRole',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'description', label: '描述', width: 400 },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ],
      editComponent: roleEdit
    };
  }
};
</script>

<style></style>
