<template>
  <el-carousel height="300px">
    <el-carousel-item v-for="(item, index) in data" :key="index">
      <el-image :src="item.src"></el-image>
    </el-carousel-item>
  </el-carousel>
</template>

<script>
export default {
  name: 'recommandPictures',
  data() {
    return {
      data: []
    };
  },
  mounted() {
    this.loadData();
  },
  methods: {
    loadData() {
      sp.get('api/RecommendInfo/GetRecommendList?type=picture').then(resp => {
        this.data = resp.map(item => ({ src: item.url }));
      });
    }
  }
};
</script>
