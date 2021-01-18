<template>
  <div class="sp-view">
    <slot />
  </div>
</template>

<script>
export default {
  name: 'sp-view',
  data() {
    return {
      headerRef: null
    };
  }
};
</script>

<style lang="less" scoped>
.sp-view {
  height: 100%;
}
</style>
