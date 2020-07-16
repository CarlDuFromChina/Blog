<template>
  <sp-section title="推荐书籍" :loading="loading">
    <el-carousel height="300px">
      <el-carousel-item v-for="(item, index) in data" :key="index">
        <el-image :src="item.src"></el-image>
      </el-carousel-item>
    </el-carousel>
  </sp-section>
</template>

<script>
export default {
  name: 'recommandPictures',
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
    loadData() {
      if (this.loading) {
        return;
      }
      this.loading = true;
      sp.get('api/RecommendInfo/GetRecommendList?type=picture')
        .then(resp => {
          this.data = resp.map(item => ({ src: item.url }));
        })
        .catch(error => {
          this.$message.error(error);
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>
