<template>
  <sp-list
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    :use-pagination="false"
  ></sp-list>
</template>

<script>
import robotEdit from './robotEdit';

export default {
  name: 'robot-list',
  data() {
    return {
      controllerName: 'robot',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '机器人名' },
        { prop: 'hook', label: '钩子地址' },
        { prop: 'robot_type_name', label: '类型' },
        { prop: 'description', label: '描述' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      editComponent: robotEdit
    };
  }
};
</script>
