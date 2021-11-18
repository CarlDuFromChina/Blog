<template>
  <div ref="header" class="sp-menu-header">
    <div class="sp-menu-header-wrapper">
      <a-row>
        <a-col>
          <ul class="sp-menu-list">
            <sp-menu-item v-for="(item, index) in menus" :key="index" @click="menuChange(item, index)" :style="{ color: getColor(index) }">
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
    return {
      currentIndex: -1
    };
  },
  mounted() {
    this.setCurrentMenu();
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
    setCurrentMenu() {
      this.menus.forEach((item, index) => {
        if (item.route === this.$route.name) {
          this.currentIndex = index;
        }
      });
    },
    getColor(index) {
      if (this.currentIndex === index) {
        return '#1e80ff';
      }
      return '#222226';
    },
    menuChange(item, index) {
      this.currentIndex = index;
      this.$emit('menu-change');
      this.$router.push({ name: item.route });
    }
  }
};
</script>

<style lang="less" scoped>
.sp-menu-header {
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
      max-width: 70%;
      margin: 0 auto;
      border-right: none;
      list-style: none;
      position: relative;
    }
  }
}
</style>
