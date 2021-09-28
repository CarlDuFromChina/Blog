<template>
  <li ref="menuItem" class="menu-item" @click="handleClick">
    <slot></slot>
  </li>
</template>

<script>
export default {
  name: 'sp-menu-item',
  props: {
    disableHover: {
      type: Boolean,
      default: false
    }
  },
  mounted() {
    if (!this.disableHover) {
      const ref = this.$refs.menuItem;
      ref.classList.add('hover');
    }
  },
  methods: {
    handleClick() {
      this.$emit('click');
    }
  }
};
</script>

<style lang="less" scoped>
.menu-item {
  float: left;
  line-height: 48px;
  padding: 0 20px;
  border-bottom: 2px solid transparent;
  transition: border-color 0.3s, background-color 0.3s, color 0.3s;
  box-sizing: border-box;
  position: relative;
  white-space: nowrap;
  list-style: none;
  cursor: pointer;
}

li {
  font-size: 14px;
  color: #222226;
}

.hover {
  &:hover {
    color: #1e80ff !important;
  }
}
</style>
