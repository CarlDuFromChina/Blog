<template>
  <a-form-model ref="form" :model="data">
    <a-form-model-item label="名称">
      <a-input v-model="data.name"></a-input>
    </a-form-model-item>
    <a-form-model-item label="编码">
      <a-input v-model="data.code" :disabled="pageState === 'edit'"></a-input>
    </a-form-model-item>
    <a-form-model-item label="索引">
      <a-input-number v-model="data.index"></a-input-number>
    </a-form-model-item>
    <a-form-model-item label="是否付费">
      <a-switch v-model="isFree"></a-switch>
    </a-form-model-item>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';

export default {
  name: 'classification-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'Classification',
      data: {
        is_free: 0
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
    }
  },
  methods: {
    postSave() {
      setTimeout(() => {
        window.location.reload();
      }, 1000);
    }
  }
};
</script>

<style></style>
