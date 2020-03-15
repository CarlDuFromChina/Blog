<template>
  <sp-blog-table :data="tableData"></sp-blog-table>
</template>

<script>
const item = {
  date: '2016-05-02',
  name: '王小虎',
  title: 'JavaScript作用域',
  content: '123'
};
export default {
  name: 'csharp',
  data() {
    return {
      tableData: Array(4).fill(item)
    };
  }
};
</script>

<style>
</style>
