<template>
  <sp-section title="推荐博客" :loading="loading">
    <el-card class="recommand">
      <a class="item" v-for="(item, index) in data" :key="index" @click="read(item)">
        <div class="entry-title">{{ item.name }}</div>
        <div class="entry-box">
          <div class="entry-meta">
            <sp-icon name="sp-blog-view" :size="15"></sp-icon>
            <span>{{ item.reading_times || 0 }}</span>
          </div>
        </div>
      </a>
    </el-card>
  </sp-section>
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
  /deep/ .el-card__body {
    padding: 0px;
  }
  .item {
    display: block;
    padding: 10px;
    cursor: pointer;
    .entry-title {
      color: #000000;
    }
    .entry-box {
      margin-top: 5px;
      .entry-meta {
        color: #c2c2c2;
      }
    }
  }
  .item:hover {
    background: hsla(0, 0%, 85.1%, 0.1);
  }
}
</style>
