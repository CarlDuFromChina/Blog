<template>
  <sp-blog-table :data="tableData"></sp-blog-table>
</template>

<script>
export default {
  name: 'js',
  data() {
    return {
      tableData: []
    };
  },
  created() {
    sp.get(`api/blog/GetDataList?searchList=${JSON.stringify(this.searchList)}`).then(resp => {
      this.tableData = resp;
    });
  },
  computed: {
    searchList() {
      return [{ Name: 'blog_type', Value: 'js' }];
    }
  }
};
</script>

<style></style>
