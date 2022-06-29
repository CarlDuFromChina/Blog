<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import editComponent from './categoryEdit.vue';

export default {
  name: 'category-list',
  data() {
    return {
      controllerName: 'category',
      editComponent,
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'index', label: '索引' },
        { prop: 'is_free_name', label: '是否付费' }
      ],
      operations: ['new', 'delete', 'search']
    };
  }
};
</script>

<style></style>
