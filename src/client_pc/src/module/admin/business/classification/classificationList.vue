<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import editComponent from './classificationEdit';

export default {
  name: 'classification-list',
  data() {
    return {
      controllerName: 'Classification',
      editComponent,
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'is_freeName', label: '是否付费' }
      ],
      operations: ['new', 'delete', 'search']
    };
  }
};
</script>

<style></style>
