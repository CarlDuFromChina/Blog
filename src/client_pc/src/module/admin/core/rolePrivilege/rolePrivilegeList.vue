<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import rolePrivilegeEdit from './rolePrivilegeEdit';

export default {
  name: 'sysRolePrivilegeList',
  data() {
    return {
      controllerName: 'SysRolePrivilege',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'sys_entityidName', label: '实体名' },
        { prop: 'description', label: '描述' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ],
      editComponent: rolePrivilegeEdit
    };
  }
};
</script>

<style></style>
