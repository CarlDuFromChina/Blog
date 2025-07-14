<template>
  <sp-card :loading="loading" class="card" title="精品文章" :empty="!listData || listData.length == 0">
    <a class="item" v-for="(item, index) in listData" :key="index" @click="read(item)">
      <div class="item-start">{{ item.title }}</div>
      <div class="item-end">
        <span>{{ item.reading_times || 0 }}</span>
      </div>
    </a>
  </sp-card>
</template>

<script>
export default {
  name: 'seriesList',
  data() {
    return {
      listData: [],
      loading: false
    };
  },
  mounted() {
    this.loadData();
  },
  methods: {
    async loadData(value = '') {
      if (this.searchValue !== value) {
        this.searchValue = value;
      }
      if (this.loading) {
        return;
      }
      this.loading = true;
      try {
        let url = 'api/post/search?orderBy=created_at desc';
        url += '&pageSize=5';
        url += '&pageIndex=1';
        url += '&searchList=';
        url += '&viewId=834F8083-47BC-42F3-A6B2-DE25BE755714';
        url += `&searchValue=${this.searchValue}`;
        const resp = await sp.get(url);
        if (resp) {
          this.listData = this.listData.concat(resp.DataList);
        }
      } catch (error) {
        this.$message.error(error);
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    read(item) {
      const { href } = this.$router.resolve({
        name: 'post',
        params: { id: item.id }
      });
      window.open(href, '_blank');
    }
  }
};
</script>

<style lang="less" scoped>
@import url('./card');
</style>
