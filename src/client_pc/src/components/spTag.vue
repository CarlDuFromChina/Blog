<template>
  <div>
    <template v-for="(tag, index) in tags">
      <a-tooltip v-if="tag.length > 20" :key="tag" :title="tag">
        <a-tag :key="tag" closable @close="() => handleClose(tag)" :color="colors[index % colors.length]">
          {{ `${tag.slice(0, 20)}...` }}
        </a-tag>
      </a-tooltip>
      <a-tag v-else :key="tag" closable @close="() => handleClose(tag)" :color="colors[index % colors.length]">
        {{ tag }}
      </a-tag>
    </template>
    <a-input
      v-if="inputVisible"
      ref="input"
      type="text"
      size="small"
      :style="{ width: '78px' }"
      :value="inputValue"
      @change="handleInputChange"
      @blur="handleInputConfirm"
      @keyup.enter="handleInputConfirm"
    />
    <a-tag v-else style="background: #fff; borderStyle: dashed;" @click="showInput"> <a-icon type="plus" /> New Tag </a-tag>
  </div>
</template>
<script>
export default {
  name: 'sp-tag',
  props: {
    tags: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {
      colors: ['pink', 'red', 'orange', 'green', 'cyan', 'blue', 'purple'],
      inputVisible: false,
      inputValue: ''
    };
  },
  methods: {
    handleClose(removedTag) {
      const tags = this.tags.filter(tag => tag !== removedTag);
      this.$emit('change', tags);
    },
    showInput() {
      this.inputVisible = true;
      this.$nextTick(function() {
        this.$refs.input.focus();
      });
    },
    handleInputChange(e) {
      this.inputValue = e.target.value;
    },
    handleInputConfirm() {
      const inputValue = this.inputValue;
      if (inputValue && this.tags.indexOf(inputValue) === -1) {
        this.$emit('change', [...this.tags, inputValue]);
      }
      Object.assign(this, {
        inputVisible: false,
        inputValue: ''
      });
    }
  }
};
</script>
