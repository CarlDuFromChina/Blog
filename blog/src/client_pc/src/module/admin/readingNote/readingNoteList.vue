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
      controllerName: 'ReadingNote',
      editComponent: readingNoteEdit,
      operations: ['new', 'delete'],
      columns: [
        { prop: 'book_title', label: '标题' },
        { prop: 'isShowName', label: '是否展示' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ]
    };
  },
  methods: {
    goEdit(item) {
      this.$router.push({
        path: `readingNote/edit/${(item || {}).Id || ''}`
      });
    }
  }
};
</script>

<style></style>
