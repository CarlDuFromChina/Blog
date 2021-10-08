<template>
  <div>
    <sp-blog-card ref="blog" :getDataApi="getDataApi" imageName="first_picture"></sp-blog-card>
    <a-spin :spinning="loading" :delay="100" style="width:100%;padding: 10px 0;text-align:center;">
      <span v-if="isLoadedAll && pageIndex > 1">到底了....</span>
    </a-spin>
  </div>
</template>

<script>
export default {
  name: 'friends',
  data() {
    return {
      controllerName: 'FriendBlog',
      loading: false,
      isLoadedAll: false
    };
  },
  mounted() {
    this.$bus.$on('loading-finish', () => {
      this.loading = false;
    });
    this.$bus.$on('loaded-all', () => {
      this.isLoadedAll = true;
    });
    this.$bus.$on('reset', () => {
      this.isLoadedAll = false;
    });
    this.$bus.$on('load-more', () => {
      if (this.isLoadedAll) {
        return;
      }
      this.loading = true;
      setTimeout(() => {
        this.$refs.blog.loadData();
      }, 500);
    });
  },
  computed: {
    pageIndex() {
      return this.$refs.blog.pageIndex;
    },
    getDataApi() {
      return `api/${this.controllerName}/GetViewData?orderBy=&viewId=F7A9536A-81E9-494F-9DF0-4AF323F1D5BC&&pageSize=$pageSize&pageIndex=$pageIndex&searchList=`;
    }
  }
};
</script>

<style lang="less" scoped></style>
