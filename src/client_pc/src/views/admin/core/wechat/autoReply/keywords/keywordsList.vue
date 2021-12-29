<template>
  <sp-list :controller-name="controllerName" :operations="operations" :columns="columns" :edit-component="editComponent"></sp-list>
</template>

<script>
import keywordsEdit from './keywordsEdit';

export default {
  name: 'materialList',
  data() {
    return {
      controllerName: 'WeChatKeywords',
      editComponent: keywordsEdit,
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '标题' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ]
    };
  }
};
</script>

<style></style>
