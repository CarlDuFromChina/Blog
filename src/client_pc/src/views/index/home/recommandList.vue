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
        const resp = await sp.get('api/RecommendInfo/GetRecommendList');
        if (resp) {
          this.data = resp;
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
      sp.get(`api/RecommendInfo/RecordReadingTimes?id=${item.id}`);
      item.reading_times = (item.reading_times || 0) + 1;
      window.open(item.url);
    }
  }
};
</script>

<style lang="less" scoped>
@import url('./card');
</style>
