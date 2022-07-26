<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import sysConfigEdit from './sysConfigEdit';

export default {
  name: 'sysConfigList',
  data() {
    return {
      controllerName: 'sys_config',
      operations: ['search', 'export'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'value', label: '值', ellipsis: true },
        { prop: 'description', label: '描述' },
      ],
      editComponent: sysConfigEdit
    };
  }
};
</script>

<style></style>
