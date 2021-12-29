<template>
  <div>
    <a-menu v-model="menu" mode="horizontal">
      <a-menu-item key="keyWords">关键词回复</a-menu-item>
      <a-menu-item key="focus">关注回复</a-menu-item>
    </a-menu>
    <keywords v-show="menu[0] == 'keyWords'"></keywords>
    <focus-reply v-show="menu[0] == 'focus'"></focus-reply>
  </div>
</template>

<script>
import keywords from './keywords/keywordsList';
import focusReply from './focusReply';

export default {
  name: 'autoReply',
  components: { keywords, focusReply },
  data() {
    return {
      menu: ['keyWords']
    };
  }
};
</script>

<style></style>
