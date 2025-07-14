<template>
  <sp-section title="推荐书籍" :loading="loading">
    <a-carousel arrows v-if="data && data.length > 0">
      <div slot="prevArrow" class="custom-slick-arrow" style="left: 10px;zIndex: 1">
        <a-icon type="left-circle" />
      </div>
      <div slot="nextArrow" class="custom-slick-arrow" style="right: 10px">
        <a-icon type="right-circle" />
      </div>
      <div v-for="(item, index) in data" :key="index"><img :src="item.src" height="300" /></div>
    </a-carousel>
    <a-empty v-else />
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
      const searchList = [{ Name: 'recommend_type', Value: "url", Type: 0 }];
      sp.get('api/recommend_info/search?searchList=' + JSON.stringify(searchList))
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

<style lang="less" scoped>
.custom-slick-arrow {
  width: 25px;
  height: 25px;
  font-size: 25px;
  color: #fff;
  background-color: rgba(31, 45, 61, 0.11);
  opacity: 0.3;
}
.ant-carousel .custom-slick-arrow:before {
  display: none;
}
.ant-carousel .custom-slick-arrow:hover {
  opacity: 0.5;
}
</style>
