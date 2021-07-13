<template>
  <div id="app" style="height:100%">
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
  height: 100%;
  margin: 0px;
  overflow: hidden;
}

#nprogress .bar {
  background: #1890ff !important;
  height: 3px !important;
}
</style>
