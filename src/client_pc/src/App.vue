<template>
  <div id="myApp" style="height:100%">
    <a-config-provider :getPopupContainer="getPopupContainer" :locale="locale">
      <router-view />
    </a-config-provider>
  </div>
</template>

<script>
import zh_CN from 'ant-design-vue/lib/locale-provider/zh_CN';

export default {
  name: 'App',
  data() {
    return {
      locale: zh_CN
    };
  },
  methods: {
    getPopupContainer(el, dialogContext) {
      if (dialogContext) {
        return dialogContext.getDialogWrap();
      } else {
        return document.body;
      }
    }
  }
};
</script>

<style lang="less">
body,
html {
  width: 100%;
  margin: 0px;
  background-color: #edeef2;
}

#nprogress .bar {
  background: #1890ff !important;
  height: 3px !important;
}
</style>
