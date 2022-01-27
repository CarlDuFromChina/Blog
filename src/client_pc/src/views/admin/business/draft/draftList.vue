<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :headerClick="goEdit"></sp-list>
</template>

<script>
export default {
  name: 'draft-edit',
  data() {
    return {
      controllerName: 'Draft',
      columns: [
        { prop: 'title', label: '标题' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      operations: ['delete', 'search']
    };
  },
  methods: {
    goEdit(item) {
      this.$router.push({
        name: 'blogEdit',
        params: {
          draftId: item.blogId
        }
      });
    }
  }
};
</script>
