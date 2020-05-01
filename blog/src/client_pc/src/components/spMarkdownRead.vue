<template>
  <div class="blog blog__readonly">
    <slot />
    <div class="blog-body">
      <div class="bodyWrapper">
        <div class="bodyWrapper-title">{{ title }}</div>
        <div class="bodyWrapper-content">
          <vue-markdown :source="content"></vue-markdown>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import VueMarkdown from 'vue-markdown';

export default {
  name: 'spMarkdownRead',
  components: { VueMarkdown },
  props: {
    title: {
      type: String,
      default: ''
    },
    content: {
      type: String,
      default: ''
    }
  }
};
</script>
