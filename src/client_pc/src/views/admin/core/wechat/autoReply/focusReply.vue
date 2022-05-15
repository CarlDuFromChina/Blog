<template>
  <a-card :bordered="false">
    <slot name="title">
      <p>
        通过编辑内容或关键词规则，快速进行自动回复设置。
        <a-switch default-checked @change="onChange" style="float:right" />
      </p>
    </slot>
    <sp-editor ref="editor" v-model="content" :enableMenu="['image', 'video']"></sp-editor>
    <a-button type="primary" style="margin-top:20px;" @click="saveData">
      保存
    </a-button>
    <a-button type="danger" @click="reset">
      清空
    </a-button>
  </a-card>
</template>

<script>
export default {
  name: 'focusReply',
  data() {
    return {
      data: {},
      content: '',
      controllerName: 'WeChatFocusReply'
    };
  },
  created() {
    this.getData().then(() => {
      this.content = this.data.content;
    });
  },
  methods: {
    onChange(checked) {
      this.data.checked = checked ? 1 : 0;
    },
    async getData() {
      return sp.get(`api/${this.controllerName}/GetData`).then(resp => (this.data = resp || {}));
    },
    saveData() {
      const operationName = sp.isNullOrEmpty(this.data.id) ? 'CreateData' : 'UpdateData';
      const url = `api/${this.controllerName}/${operationName}`;
      if (operationName === 'CreateData') {
        this.data.id = uuid.generate();
      }
      this.data.content = this.$refs.editor.editor.txt.text();
      sp.post(url, this.data)
        .then(() => {
          this.$message.success('保存成功');
        })
        .catch(() => {
          this.$message.error('保存失败');
        });
    },
    reset() {
      this.content = '';
      this.saveData();
    }
  }
};
</script>

<style></style>
