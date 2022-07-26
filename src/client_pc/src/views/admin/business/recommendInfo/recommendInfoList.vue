<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent"></sp-list>
</template>

<script>
import recommendInfoEdit from './recommendInfoEdit';

export default {
  name: 'RecommendInfoList',
  data() {
    return {
      controllerName: 'recommend_info',
      editComponent: recommendInfoEdit,
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'recommend_type_name', label: '类型' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      operations: ['new', 'delete', 'search']
    };
  }
};
</script>
