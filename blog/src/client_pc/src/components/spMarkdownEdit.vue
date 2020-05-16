<template>
  <div class="blog blog__edit">
    <slot name="header"></slot>
    <div class="blog-body">
      <div class="blog-bodywrapper">
        <div class="blog-bodywrapper-markdown">
          <div class="blog-bodywrapper-markdown-container">
            <mavon-editor v-model="model" ref="md" @imgAdd="imgAdd" @change="change" style="min-height: 600px;height:100%" />
          </div>
        </div>
      </div>
    </div>
    <slot name="dialog"></slot>
  </div>
</template>

<script>
import { mavonEditor } from 'mavon-editor';
import 'mavon-editor/dist/css/index.css';

export default {
  name: 'sp-markdown-edit',
  components: { mavonEditor },
  props: {
    value: {
      type: String
    },
    imgAdd: {
      type: Function
    }
  },
  computed: {
    model: {
      get() {
        return this.value;
      },
      set(newVal) {
        this.$emit('input', newVal);
      }
    }
  },
  methods: {
    change(value, render) {
      this.$emit('change', render); // render 为 markdown 解析后的结果[html]
    }
  }
};
</script>
