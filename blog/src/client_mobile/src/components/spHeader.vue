<template>
  <mt-header ref="header" fixed :title="title">
    <mt-button icon="back" @click="$router.back()" slot="left">返回</mt-button>
    <mt-button icon="more" slot="right">
      <slot name="right"></slot>
    </mt-button>
  </mt-header>
</template>

<script>
export default {
  name: 'sp-header',
  props: {
    title: {
      type: String,
      default: ''
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$parent.headerRef = this.$refs.header;
    });
  },
  destroyed() {
    this.$parent.headerRef = null;
  }
};
</script>
