<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import rolePrivilegeEdit from './rolePrivilegeEdit';

export default {
  name: 'sysRolePrivilegeList',
  data() {
    return {
      controllerName: 'sys_role_rivilege',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'objectid_name', label: '实体名' },
        { prop: 'description', label: '描述' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      editComponent: rolePrivilegeEdit
    };
  }
};
</script>

<style></style>
