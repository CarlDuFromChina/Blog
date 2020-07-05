<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    allow-select
  ></sp-list>
</template>

<script>
import recommendInfoEdit from './RecommendInfoEdit';

export default {
  name: 'RecommendInfoList',
  data() {
    return {
      controllerName: 'RecommendInfo',
      editComponent: recommendInfoEdit,
      columns: [
        { prop: 'name', label: '名称', width: 300 },
        { prop: 'url', label: '地址', width: 500 },
        { prop: 'recommend_typeName', label: '类型' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ],
      operations: ['new', 'delete', 'search']
    };
  }
};
</script>
