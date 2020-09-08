<template>
  <sp-blog-card :getDataApi="getDataApi"></sp-blog-card>
</template>

<script>
export default {
  name: 'friends',
  data() {
    return {
      controllerName: 'FriendBlog'
    };
  },
  computed: {
    getDataApi() {
      return `api/${this.controllerName}/GetDataList?orderBy=&viewId=F7A9536A-81E9-494F-9DF0-4AF323F1D5BC&&pageSize=$pageSize&pageIndex=$pageIndex&searchList=`;
    }
  }
};
</script>

<style lang="less" scoped></style>
