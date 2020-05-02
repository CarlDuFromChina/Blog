<template>
  <div class="blog blog__readonly">
    <slot />
    <div class="blog-body" :style="{ 'background-color': backgroudColor }">
      <div class="bodyWrapper">
        <div class="bodyWrapper-title">{{ title }}</div>
        <div class="bodyWrapper-content">
          <div v-highlight v-html="formatterContent"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import marked from 'marked';
import 'mavon-editor/dist/css/index.css';

export default {
  name: 'spMarkdownRead',
  props: {
    title: {
      type: String,
      default: ''
    },
    content: {
      type: String,
      default: ''
    },
    backgroudColor: {
      type: String,
      default: '#e9ecef'
    }
  },
  data() {
    return {
      formatterContent: ''
    };
  },
  watch: {
    content: {
      immediate: true,
      handler(newVal) {
        if (!sp.isNullOrEmpty(newVal)) {
          this.formatterContent = marked(newVal, {
            sanitize: true
          });
        }
      }
    }
  }
};
</script>
