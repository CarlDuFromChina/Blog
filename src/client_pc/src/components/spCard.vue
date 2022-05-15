<template>
  <div class="card">
    <a-skeleton :loading="loading" active>
      <slot name="title">
        <h3 v-if="title">{{ title }}</h3>
      </slot>
      <slot></slot>
      <a-empty v-if="empty" />
    </a-skeleton>
  </div>
</template>

<script>
export default {
  name: 'spCard',
  props: {
    title: {
      type: String
    },
    empty: {
      type: Boolean,
      default: false
    },
    loading: {
      type: Boolean,
      default: true
    }
  }
};
</script>

<style lang="less" scoped>
.card {
  background-color: white;
  border-radius: 4px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.05), 0 0 1px rgba(0, 0, 0, 0.1);
  color: #4a4a4a;
  max-width: 100%;
  position: relative;
  padding: 1.5rem;
  .title {
    color: #7a7a7a;
    font-size: 0.75em;
    letter-spacing: 0.1em;
    text-transform: uppercase;
  }
}
</style>
