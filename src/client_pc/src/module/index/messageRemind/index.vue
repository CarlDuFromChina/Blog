<template>
  <sp-card class="message">
    <a-tabs default-active-key="1" :animated="false">
      <a-tab-pane key="1" tab="点赞提醒">
        <message-list view="upvote" key="1"> </message-list>
      </a-tab-pane>
      <a-tab-pane key="2" tab="评论消息">
        <message-list view="comment" key="2"> </message-list>
      </a-tab-pane>
      <a-tab-pane key="3" tab="系统消息">
        <message-list view="system" key="3"> </message-list>
      </a-tab-pane>
    </a-tabs>
  </sp-card>
</template>

<script>
import messageList from './messageList.vue';

export default {
  name: 'messageRemind',
  components: { messageList }
};
</script>

<style lang="less" scoped>
.message {
  padding: 0 1.5rem 1.5rem 1.5rem !important;
}
</style>
