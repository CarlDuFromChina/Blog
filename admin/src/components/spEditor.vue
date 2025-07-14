<!--
 * @FileDescription: 富文本编辑器（wangEditor）
 * @Author: Karl Du
 * @Date: 2020/10/29
 -->
<template>
  <div ref="editor"></div>
</template>

<script>
import Vue from 'vue';
import E from 'wangeditor';
const { BtnMenu } = E;

class SaveMenu extends BtnMenu {
  constructor(editor) {
    const MyComponent = Vue.extend({
      template: `<div class="w-e-menu"><a-icon type="save" style="color: #999"/></div>`,
    });
    const component = new MyComponent().$mount();
    var o = document.createElement("div");
    o.appendChild(component.$el);
    super(E.$(o.innerHTML), editor);
  }

  clickHandler() {
    this.editor.save();
  }

  tryChangeActive() {
    this.active();
  }
}

export default {
  name: 'sp-editor',
  props: {
    value: {
      type: String,
      default: ''
    },
    // 请求额外参数
    uploadImgParams: {
      type: Object,
      default: () => {}
    },
    // 禁用功能
    disabledMenu: {
      type: Array,
      default: () => []
    },
    // 使用功能
    enableMenu: {
      type: Array,
      default: () => []
    },
    // 创建事件
    preCreate: {
      type: Function
    }
  },
  model: {
    prop: 'value',
    event: 'input'
  },
  data() {
    return {
      editor: {},
      isChange: false
    };
  },
  watch: {
    value() {
      if (!this.isChange) {
        this.editor.txt.html(this.value);
        this.isChange = true;
      }
    },
    uploadImgParams() {
      this.editor.config.uploadImgParams = this.uploadImgParams;
    }
  },
  mounted() {
    this.editor = new E(this.$refs.editor);
    this.editor.config.onchange = html => {
      this.$emit('input', html);
    };
    if (this.enableMenu && this.enableMenu.length > 0) {
      this.editor.config.menus = this.enableMenu;
    }
    if (this.preCreate && typeof this.preCreate === 'function') {
      this.preCreate(this.editor);
    }
    this.editor.config.menus = this.editor.config.menus.filter(item => !this.disabledMenu.includes(item));
    this.editor.config.uploadImgParamsWithUrl = true;
    this.editor.config.uploadImgHeaders = {
      Authorization: `Bearer ${localStorage.getItem('Token') || ''}`
    };
    this.editor.config.uploadImgMaxLength = 1; // 最多一次上传一张照片
    this.editor.config.uploadImgServer = `${sp.getServerUrl()}api/sys_file/upload_image`; // 上传图片服务地址
    this.editor.config.uploadImgHooks = {
      customInsert: function (insertImgFn, result) {
        insertImgFn(sp.getDownloadUrl(result.downloadUrl));
      }
    };
    this.editor.customConfig = {
      onchange: html => {
        this.isChange = true;
        this.$emit('input', html);
      }
    };
    this.editor.menus.extend('saveMenu', SaveMenu);
    this.editor.config.menus = this.editor.config.menus.concat('saveMenu');
    this.editor.config.zIndex = 100;
    this.editor.save = () => this.$emit('save-data');
    this.editor.create();
  }
};
</script>

<style lang="less" scoped>
.w-e-full-screen-editor {
  z-index: 100 !important;
}
</style>