<template>
  <sp-list
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :headerClick="goEdit"
  ></sp-list>
</template>

<script>
export default {
  name: 'readingNoteList',
  data() {
    return {
      controllerName: 'reading_note',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'book_title', label: '标题' },
        { prop: 'is_show_name', label: '是否展示' },
        { prop: 'created_by_name', label: '创建人' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' },
        { prop: 'action', label: '操作', type: 'actions', actions: [{ name: '查看', size: 'small', method: this.goReadonly }] }
      ]
    };
  },
  methods: {
    goEdit(item) {
      var host = process.env.VUE_APP_INDEX_URL;
      window.open(`${host}/reading-note/${item.id}`, '_blank');
    },
    goReadonly(item) {
      var host = process.env.VUE_APP_AXIOS_BASE_URL;
      window.open(`${host}/post/${item.id}`, '_blank');
      const { href } = this.$router.resolve({
        name: 'readingNote',
        params: { id: item.id }
      });
      window.open(href, '_blank');
    }
  }
};
</script>

<style></style>
