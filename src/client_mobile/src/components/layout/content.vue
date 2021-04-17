<template>
  <div class="sp-content" ref="content" :style="contentStyle">
    <slot></slot>
  </div>
</template>

<script>
export default {
  data() {
    return {
      contentStyle: {}
    };
  },
  watch: {
    '$parent.headerRef.clientHeight': function(newVal) {
      this.resizeStyle();
    },
    '$parent.footerRef.clientHeight': function(newVal) {
      this.resizeStyle();
    }
  },
  mounted() {
    this.resizeStyle();
  },
  methods: {
    resizeStyle() {
      this.$nextTick(() => {
        const footerHeight = this.$parent.footerRef ? this.$parent.footerRef.clientHeight : 0;
        const headerHeight = this.$parent.headerRef ? this.$parent.headerRef.clientHeight : 0;
        this.contentStyle = {
          height: `calc(100% - ${headerHeight}px - ${footerHeight}px)`,
          top: `${headerHeight}px`
        };
      });
    }
  }
};
</script>

<style lang="less" scoped>
.sp-content {
  position: absolute;
  left: 0;
  right: 0%;
  overflow-x: hidden;
  overflow-y: auto;
  height: 100%;
  width: 100%;
}
</style>
