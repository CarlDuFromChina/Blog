<template>
  <div class="sp-menu-header">
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
  .sp-menu-header-wrapper {
    width: 100%;
    background: rgba(40, 42, 44, 0.6);
    /*margin-bottom:30px;*/
    -webkit-box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.12), 0 0 6px 0 rgba(0, 0, 0, 0.04);
    box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.12), 0 0 6px 0 rgba(0, 0, 0, 0.04);
    position: fixed;
    left: 0;
    top: 0;
    right: 0;
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
