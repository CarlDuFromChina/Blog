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
      controllerName: 'SysMenu',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '菜单名' },
        { prop: 'router', label: '路由' },
        { prop: 'menu_Index', label: '索引' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'modifiedByName', label: '最后修改人' },
        { prop: 'modifiedOn', label: '最后修改日期', type: 'datetime' },
        { prop: 'stateCodeName', label: '状态' }
      ],
      editComponent: sysMenuEdit
    };
  },
  computed: {
    customApi() {
      return 'api/SysMenu/GetDataList';
    }
  }
};
</script>
