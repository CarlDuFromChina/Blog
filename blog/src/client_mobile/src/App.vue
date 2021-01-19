<template>
  <div id="app" style="height:100%;">
    <router-view class="router-view" />
  </div>
</template>

<script>
export default {
  name: 'App'
};
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

html {
  height: 100%;
  overflow-x: hidden;
}

body {
  height: 100%;
  overflow-x: hidden;
}

.router-view {
  height: 100%;
  width: 100%;
}
</style>
