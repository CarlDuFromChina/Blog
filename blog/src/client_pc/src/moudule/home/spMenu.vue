<template>
  <div class="sp-menu-header">
    <div class="sp-menu-header-wrapper">
      <el-row>
        <el-col>
          <ul class="el-menu sp-menu-list el-menu--horizontal">
            <li class="el-menu-item" v-for="(item, index) in menus" :key="index" @click="item.click || 'javascript:void(0)'">
              {{ item.name }}
            </li>
            <slot name="menus"></slot>
          </ul>
        </el-col>
      </el-row>
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
      default: () => [{ name: '首页' }, { name: '归档' }, { name: '友人帐' }, { name: '关于' }]
    }
  },
  data() {
    return {};
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
      > li {
        font-size: 16px;
        color: #fff;
      }
      > li:hover {
        background: #48456c;
      }
    }
  }
}
</style>
