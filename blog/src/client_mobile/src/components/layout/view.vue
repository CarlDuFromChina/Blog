<template>
  <div class="sp-view">
    <slot />
  </div>
</template>

<script>
export default {
  data() {
    return {
      headerRef: null,
      footerRef: null
    };
  }
};
</script>

<style lang="less" scoped>
.sp-view {
  height: 100%;
}
</style>
