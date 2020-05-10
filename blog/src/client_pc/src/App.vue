<template>
  <div id="app" style="height:100%">
    <router-view />
  </div>
</template>

<script>
export default {
  name: 'App',
  created() {
    sp.get('api/DataService/test').then(resp => {
      // 获取菜单，返回首页
      sp.refreshRouter.call({ router: this.$router });
      sp.goHome();
    });
  }
};
</script>

<style lang="less">
body,
html {
  width: 100%;
  height: 100%;
  margin: 0px;
}
</style>
