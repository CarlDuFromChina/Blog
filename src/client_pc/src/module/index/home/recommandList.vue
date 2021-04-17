<template>
  <sp-card :loading="loading" class="recommand" title="好文推荐" :empty="!data || data.length == 0">
    <a class="item" v-for="(item, index) in data" :key="index" @click="read(item)">
      <div class="item-start">{{ item.name }}</div>
      <div class="item-end">
        <span><a-icon type="eye" style="margin-right: 8px" />{{ item.reading_times || 0 }}</span>
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
      sp.get(`api/RecommendInfo/RecordReadingTimes?id=${item.Id}`);
      item.reading_times = (item.reading_times || 0) + 1;
      window.open(item.url);
    }
  }
};
</script>

<style lang="less" scoped>
.recommand {
  /deep/ .ant-card-body {
    padding: 0px;
  }
  .item {
    display: flex;
    padding: 10px;
    cursor: pointer;
    align-items: center;
    justify-content: space-between;
    &-start {
      color: #000000;
    }
    &-end {
      color: #4a4a4a;
    }
  }
  .item:hover {
    background: hsla(0, 0%, 85.1%, 0.1);
  }
}
</style>
