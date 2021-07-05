<template>
  <a-switch v-model="model" v-bind="$attrs" v-on="$listeners"></a-switch>
</template>

<script>
export default {
  name: 'sp-switch',
  model: {
    prop: 'value',
    event: 'input'
  },
  props: {
    value: {
      type: Number,
      required: true
    }
  },
  computed: {
    model: {
      get() {
        return this.value === 1;
      },
      set(value) {
        this.$emit('input', value);
      }
    }
  }
};
</script>
