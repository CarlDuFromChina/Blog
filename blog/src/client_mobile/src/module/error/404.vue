<template>
  <div :title="title" class="error">
    <div class="img-box">
      <img src="../../assets/imgs/404.png" alt="" />
      <div class="font-box">
        <p>抱歉，您访问的页面不存在...</p>
        <span>错误代码：404</span>
      </div>
      <mt-button class="button-style" type="primary" @click="goBack()">返回</mt-button>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      title: '404'
    };
  },
  methods: {
    goBack() {
      this.$router.back();
    }
  }
};
</script>

<style lang="less" scoped>
.error {
  width: 100%;
  height: 100%;
  text-align: center;
  vertical-align: middle;
}

.button-style {
  width: 140px;
  height: 30px;
  border-radius: 20px;
  border: none;
  color: #fff !important;
  background: #09f;
  cursor: pointer;
}

.img-box {
  padding-top: 60px;
}

.img-box p {
  font-size: 20px;
  color: #555;
  margin-bottom: 14px;
  font-family: 'Microsoft YaHei';
}

.img-box img {
  max-width: 100%;
}

.font-box {
  margin-bottom: 40px;
}

.font-box span {
  font-size: 16px;
  font-family: 'Microsoft YaHei';
  color: #888 !important;
}
</style>
