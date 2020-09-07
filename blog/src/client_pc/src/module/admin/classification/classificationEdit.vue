<template>
  <a-form-model ref="form" :model="data">
    <a-form-model-item label="名称">
      <a-input v-model="data.name"></a-input>
    </a-form-model-item>
    <a-form-model-item label="编码">
      <a-input v-model="data.code"></a-input>
    </a-form-model-item>
    <a-form-model-item label="是否付费">
      <a-switch v-model="isFree"></a-switch>
    </a-form-model-item>
    <a-form-model-item label="是否前台显示">
      <a-switch v-model="isShow"></a-switch>
    </a-form-model-item>
  </a-form-model>
</template>

<script>
import { edit } from 'sixpence.platform.pc.vue';

export default {
  name: 'classification-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'Classification',
      data: {
        is_free: 0,
        is_show: 1
      }
    };
  },
  computed: {
    isFree: {
      get() {
        return !!this.data.is_free;
      },
      set(val) {
        this.data.is_free = val ? 1 : 0;
      }
    },
    isShow: {
      get() {
        return !!this.data.is_show;
      },
      set(val) {
        this.data.is_show = val ? 1 : 0;
      }
    }
  }
};
</script>

<style></style>
