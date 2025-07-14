<template>
  <div class="errorView">
    <slot>
      <div class="img">
        <slot name="img">
          <img :src="typeItem.src" alt="" />
        </slot>
        <p class="info">
          <slot name="info">
            {{ typeItem.errorMessage }}
          </slot>
        </p>
      </div>
    </slot>
  </div>
</template>

<script>
const types = [
  { name: 'fail', src: require('../assets/imgs/fail.png'), errorMessage: '系统开小差了~' },
  { name: 'no-content', src: require('../assets/imgs/nocontent.png'), errorMessage: '暂无相关内容' },
  { name: 'no-search', src: require('../assets/imgs/nosearch.png'), errorMessage: 'Sorry，没有搜索到相关信息~' }
];

export default {
  name: 'error',
  props: {
    type: {
      type: String,
      required: true,
      default: 'fail'
    }
  },
  data() {
    return {
      parentHeight: 0,
      types
    };
  },
  computed: {
    typeItem() {
      return this.types.find(item => item.name === this.type);
    },
    imgStyle() {
      return {
        /* 页面高度小于 305 则自动隐藏图片 */
        display: this.parentHeight > 305 ? '' : 'none'
      };
    }
  },
  mounted() {
    setTimeout(() => {
      this.$nextTick(() => {
        this.parentHeight = this.$parent.$el.offsetHeight;
      });
    }, 0);
  }
};
</script>

<style lang="less">
.errorView {
  padding-top: 30%;
  text-align: center;
  margin: 0 52px;
  .img {
    position: relative;
    background-size: 100% 100%;
    height: 100%;
    width: 100%;

    img {
      width: 100%;
      height: 100%;
    }
  }
  .info {
    position: absolute;
    text-align: center;
    color: #b4bdcf;
    width: 100%;
    font-size: 13px;
    bottom: -20px;
    line-height: 1;
  }
}
</style>
