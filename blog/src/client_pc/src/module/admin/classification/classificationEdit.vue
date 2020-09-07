<template>
  <a-form-model ref="form" :model="data">
    <a-form-model-item label="名称">
      <a-input v-model="data.name"></a-input>
    </a-form-model-item>
    <a-form-model-item label="编码">
      <a-input v-model="data.code"></a-input>
    </a-form-model-item>
    <a-form-model-item label="是否付费">
      <a-switch v-model="is_free"></a-switch>
    </a-form-model-item>
    <a-form-model-item label="是否前台显示">
      <a-switch v-model="is_show"></a-switch>
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
      is_free: false,
      is_show: true,
      data: {
        is_free: 0,
        is_show: 1
      }
    };
  },
  computed: {
    isFree: {
      get() {
        return this.is_free || !!this.data.is_free;
      },
      set(val) {
        this.data.is_free = val ? 0 : 1;
        this.is_free = val;
      }
    },
    isShow: {
      get() {
        return this.is_show || !!this.data.is_show;
      },
      set(val) {
        this.data.is_show = val ? 1 : 0;
        this.is_show = val;
      }
    }
  }
};
</script>

<style></style>
