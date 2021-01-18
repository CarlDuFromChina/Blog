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
    }
  },
  mounted() {
    this.resizeStyle();
  },
  methods: {
    resizeStyle() {
      this.$nextTick(() => {
        this.contentStyle = {
          height: `calc(100% - ${this.$parent.headerRef ? this.$parent.headerRef.$el.clientHeight : 0}px)`,
          top: `${this.$parent.headerRef ? this.$parent.headerRef.$el.clientHeight : 0}px`
        };
      });
    }
  }
};
</script>

<style></style>
