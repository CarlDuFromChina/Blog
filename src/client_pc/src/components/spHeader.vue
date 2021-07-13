<template>
  <div class="header">
    <a-breadcrumb style="line-height:60px;margin-left:10px;">
      <a-breadcrumb-item href="">
        <a-icon type="home" />
      </a-breadcrumb-item>
      <a-breadcrumb-item href="">
        <span>{{ $route.meta.title }}</span>
      </a-breadcrumb-item>
    </a-breadcrumb>
    <slot />
  </div>
</template>

<script>
export default {
  name: 'spHeader',
  data() {
    return {};
  }
};
</script>

<style lang="less" scoped>
.header {
  display: flex;
  justify-content: space-between;
  line-height: 60px;
  height: 60px;
}
</style>
