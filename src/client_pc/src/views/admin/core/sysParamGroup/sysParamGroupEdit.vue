<template>
  <a-form-model ref="form" :model="data" label-width="80px" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="编码" prop="code">
          <a-input v-model="data.code"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row>
      <a-col>
        <sys-param-list :relatedAttr="relatedAttrs"></sys-param-list>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';
import sysParamList from './sysParamList';

export default {
  name: 'sysParamGroupEdit',
  components: { sysParamList },
  props: {
    isGrid: {
      type: Boolean,
      default: false
    }
  },
  mixins: [edit],
  data() {
    return {
      controllerName: 'sys_param_group',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'Null' }],
        code: [{ required: true, message: '请再次编码', trigger: 'Null' }]
      }
    };
  },
  computed: {
    relatedAttrs() {
      return {
        id: this.data.id,
        name: this.data.name
      };
    }
  }
};
</script>
