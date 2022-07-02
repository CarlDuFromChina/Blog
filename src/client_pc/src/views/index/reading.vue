<template>
  <div>
    <sp-blog-card ref="list" :getDataApi="getDataApi" authorName="created_by_name" :handleReadClick="handleReadClick"></sp-blog-card>
    <a-spin :spinning="loading" :delay="100" style="width:100%;padding: 10px 0;text-align:center;">
      <span v-if="isLoadedAll && pageIndex > 1">到底了....</span>
    </a-spin>
  </div>
</template>

<script>
export default {
  name: 'readingNoteList',
  data() {
    return {
      controllerName: 'reading_note',
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
        this.$refs.list.loadData();
      }, 500);
    });
  },
  computed: {
    pageIndex() {
      return this.$refs.list.pageIndex;
    },
    getDataApi() {
      return `api/${this.controllerName}/search?orderBy=&viewId=03860DF4-0E9E-4330-80BF-6A1E9AC797A6&pageSize=$pageSize&pageIndex=$pageIndex&searchList=`;
    }
  },
  methods: {
    handleReadClick(item) {
      const { href } = this.$router.resolve({
        name: 'readingNoteReadonly',
        params: { id: item.id }
      });
      window.open(href, '_blank');
    }
  }
};
</script>

<style lang="less" scoped></style>
