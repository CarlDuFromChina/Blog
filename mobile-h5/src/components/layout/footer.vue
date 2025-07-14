<template>
  <div class="footer" ref="footer">
    <slot />
  </div>
</template>

<script>
export default {
  mounted() {
    this.$nextTick(() => {
      this.$parent.footerRef = this.$refs.footer;
    });
  },
  destroyed() {
    this.$parent.footerRef = null;
    this.$bus.$off('resize-footer');
  }
};
</script>

<style lang="less" scoped>
.footer {
  position: absolute;
  bottom: 0;
  width: 100%;
}
</style>
