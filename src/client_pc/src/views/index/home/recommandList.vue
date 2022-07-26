<template>
  <sp-card :loading="loading" class="card" title="好文推荐" :empty="!data || data.length == 0">
    <a class="item" v-for="(item, index) in data" :key="index" @click="read(item)">
      <div class="item-start">{{ item.name }}</div>
      <div class="item-end">
        <span>{{ item.reading_times || 0 }}</span>
      </div>
    </a>
  </sp-card>
</template>

<script>
export default {
  name: 'recommandList',
  data() {
    return {
      data: [],
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
        const searchList = [{ Name: 'recommend_type', Value: "url", Type: 0 }];
        const resp = await sp.get('api/recommend_info/search?pageindex=1&pagesize=5&searchList=' + JSON.stringify(searchList));
        if (resp) {
          this.data = resp.DataList;
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
      sp.get(`api/recommend_info/reading_times?id=${item.id}`);
      item.reading_times = (item.reading_times || 0) + 1;
      window.open(item.url);
    }
  }
};
</script>

<style lang="less" scoped>
@import url('./card');
</style>
