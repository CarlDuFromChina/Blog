<template>
  <div class="svg-icon" @click="click" :style="{ 'cursor': cursor }">
    <a-tooltip :title="tooltip">
      <svg :width="size" :height="size" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" aria-hidden="true">
        <use :xlink:href="'#' + name" :fill="color" />
      </svg>
    </a-tooltip>
  </div>
</template>

<script>
export default {
  name: 'spIcon',
  props: {
    name: {
      type: String
    },
    size: {
      type: [Number, String],
      default: 14
    },
    color: {
      type: String,
      default: 'currentColor'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    cursor: {
      type: String,
      default: 'default'
    },
    tooltip: {
      type: String
    }
  },
  methods: {
    click(event) {
      if (!this.disabled) this.$emit('click', event);
    }
  }
};
</script>

<style lang="less">
.svg-icon {
  fill: currentColor;
  overflow: hidden;
  display: inline-block;
  vertical-align: middle;
  vertical-align: middle;
  & > svg:focus {
    outline: none;
  }
}
</style>
