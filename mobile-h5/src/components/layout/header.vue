<template>
  <mt-header ref="header" fixed :title="title">
    <mt-button icon="back" @click="$router.back()" slot="left" v-if="back">返回</mt-button>
    <mt-button icon="more" slot="right">
      <slot name="right"></slot>
    </mt-button>
  </mt-header>
</template>

<script>
export default {
  props: {
    title: {
      type: String,
      default: ''
    },
    back: {
      type: Boolean,
      default: true
    }
  },
  mounted() {
    this.$nextTick(() => {
      this.$parent.headerRef = this.$refs.header.$el;
    });
  },
  destroyed() {
    this.$parent.headerRef = null;
  }
};
</script>
