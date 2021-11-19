<template>
  <div id="myApp" style="height:100%">
    <router-view />
  </div>
</template>

<script>
export default {
  name: 'App'
};
</script>

<style lang="less">
body,
html {
  width: 100%;
  margin: 0px;
  background-color: #edeef2;
}

#nprogress .bar {
  background: #1890ff !important;
  height: 3px !important;
}
</style>
