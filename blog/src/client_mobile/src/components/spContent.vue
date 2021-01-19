<template>
  <div class="sp-content" ref="content" :style="contentStyle">
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: 'sp-content',
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

<style></style>
