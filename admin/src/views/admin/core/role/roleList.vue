<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import roleEdit from './roleEdit';

export default {
  name: 'sysRoleList',
  data() {
    return {
      controllerName: 'sys_role',
      operations: ['new', 'delete', 'search', 'export'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'description', label: '描述', width: 400 },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      editComponent: roleEdit
    };
  }
};
</script>
