<template>
  <div ref="header" class="sp-menu-header">
    <div class="sp-menu-header-wrapper">
      <a-row>
        <a-col>
          <ul class="sp-menu-list">
            <sp-menu-item v-for="(item, index) in menus" :key="index" @click="menuChange(item)">
              {{ item.name }}
            </sp-menu-item>
            <slot name="menus"></slot>
          </ul>
        </a-col>
      </a-row>
    </div>
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: 'spMenu',
  props: {
    menus: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {};
  },
  mounted() {
    this.$bus.$on('scroll', val => {
      const ref = this.$refs.header;
      if (val !== 0) {
        ref.style.position = 'fixed';
        ref.style.width = '100%';
        ref.style.zIndex = 99;
        ref.style.top = 0;
      } else {
        ref.style.position = 'relative';
      }
    });
  },
  destroyed() {
    this.$bus.$off('scroll');
  },
  methods: {
    menuChange(item) {
      this.$emit('menu-change');
      item.click();
    }
  }
};
</script>

<style lang="less" scoped>
.sp-menu-header {
  // transition: all 0.2s;
  // transform: translate3d(0, -100%, 0);
  box-shadow: 0 2px 4px 0 rgb(0 0 0 / 5%);
  color: #222226;
  background: #fff;
  font-size: 14px;
  font-weight: 400;
  .sp-menu-header-wrapper {
    width: 100%;
    z-index: 100;
    .sp-menu-list {
      background: transparent;
      border-bottom: none !important;
      max-width: 80%;
      margin: 0 auto;
      border-right: none;
      list-style: none;
      position: relative;
    }
  }
}
</style>
