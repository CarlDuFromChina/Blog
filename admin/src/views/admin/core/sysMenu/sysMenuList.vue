<template>
  <sp-list
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    :customApi="customApi"
    :use-pagination="false"
  ></sp-list>
</template>

<script>
import sysMenuEdit from './sysMenuEdit';

export default {
  name: 'sysMenuList',
  data() {
    return {
      controllerName: 'sys_menu',
      operations: ['new', 'delete', 'export'],
      columns: [
        { prop: 'name', label: '菜单名' },
        { prop: 'router', label: '路由' },
        { prop: 'menu_index', label: '索引' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' },
        { prop: 'updated_by_name', label: '最后修改人' },
        { prop: 'updated_at', label: '最后修改日期', type: 'datetime' },
        { prop: 'statecode_name', label: '状态' }
      ],
      editComponent: sysMenuEdit
    };
  },
  computed: {
    customApi() {
      return 'api/sys_menu/search';
    }
  }
};
</script>
