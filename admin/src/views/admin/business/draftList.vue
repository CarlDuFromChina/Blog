<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :headerClick="goEdit"></sp-list>
</template>

<script>
export default {
  name: 'draft-edit',
  data() {
    return {
      controllerName: 'draft',
      columns: [
        { prop: 'title', label: '标题' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      operations: ['delete', 'search']
    };
  },
  methods: {
    goEdit(item) {
      var router = this.$router.resolve({
        name: 'draftEdit',
        params: {
          draftId: item.id
        }
      });
      window.open(router.href, '_blank')
    }
  }
};
</script>
