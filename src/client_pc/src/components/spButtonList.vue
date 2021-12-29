<template>
  <div>
    <template v-for="(button, index) in buttons">
      <a-input-search
        v-if="button.name === 'search'"
        placeholder="请输入内容"
        style="width: 200px;margin-right:10px;"
        @search="onSearch"
        v-bind:key="index"
      />
      <a-button v-else-if="button.name === 'more'" type="link" v-bind:key="index" @click="showMore" style="margin-right:10px;">{{
        moreLabel
      }}</a-button>
      <a-button v-else :icon="button.icon" @click="handleClick(button)" v-bind:key="index" style="margin-right:10px;">{{ button.text }}</a-button>
    </template>
  </div>
</template>

<script>
export default {
  name: 'spButtonList',
  props: {
    // 按钮
    buttons: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {
      more: false,
      moreLabel: '展开筛选'
    };
  },
  methods: {
    showMore() {
      this.more = !this.more;
      if (this.more) {
        this.moreLabel = '收起筛选';
        this.$emit('unfold');
      } else {
        this.moreLabel = '展开更多';
        this.$emit('fold');
      }
    },
    handleClick(button) {
      if (button.operate && typeof button.operate === 'function') {
        button.operate();
      }
    },
    onSearch(value) {
      this.$emit('search-change', value);
    }
  }
};
</script>
