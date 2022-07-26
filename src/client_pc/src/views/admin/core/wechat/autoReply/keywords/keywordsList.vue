<template>
  <sp-list :controller-name="controllerName" :operations="operations" :columns="columns" :edit-component="editComponent"></sp-list>
</template>

<script>
import keywordsEdit from './keywordsEdit';

export default {
  name: 'materialList',
  data() {
    return {
      controllerName: 'wechat_keywords',
      editComponent: keywordsEdit,
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '标题' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ]
    };
  }
};
</script>

<style></style>
