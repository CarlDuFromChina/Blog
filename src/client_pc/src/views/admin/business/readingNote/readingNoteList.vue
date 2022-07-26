<template>
  <sp-list
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :edit-component="editComponent"
    :headerClick="goEdit"
  ></sp-list>
</template>

<script>
import readingNoteEdit from './readingNoteEdit';

export default {
  name: 'readingNoteList',
  data() {
    return {
      controllerName: 'reading_note',
      editComponent: readingNoteEdit,
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
      this.$router.push({
        path: `readingNote/edit/${(item || {}).id || ''}`
      });
    },
    goReadonly(item) {
      const { href } = this.$router.resolve({
        name: 'readingNoteReadonly',
        params: { id: item.id }
      });
      window.open(href, '_blank');
    }
  }
};
</script>

<style></style>
