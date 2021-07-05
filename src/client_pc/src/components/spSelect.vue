<template>
  <a-select v-model="selectModel">
    <a-select-option v-model="item.Value" v-for="(item, index) in options" :key="index">{{ item.Name }}</a-select-option>
  </a-select>
</template>

<script>
export default {
  name: 'sp-select',
  model: {
    prop: 'value',
    event: 'input'
  },
  props: {
    value: {
      type: String,
      default: '',
      required: true
    },
    options: {
      type: Array,
      default: () => [],
      required: true
    }
  },
  computed: {
    selectModel: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit('input', value);
        const arrs = this.options.filter(item => item.Value === value);
        if (arrs.length > 0) {
          this.$emit('change', {
            value,
            name: arrs[0].Name
          });
        }
      }
    }
  }
};
</script>
