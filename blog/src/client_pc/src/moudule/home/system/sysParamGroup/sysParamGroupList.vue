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
        { prop: 'code', label: '编码' }
      ],
      editComponent: sysParamEdit
    };
  }
};
</script>

<style></style>
