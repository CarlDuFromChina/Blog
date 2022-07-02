<template>
  <sp-card class="message" :loading="false">
    <a-tabs default-active-key="1" :animated="false" @change="clearCount">
      <a-tab-pane key="1">
        <a-badge slot="tab" :dot="upvote > 0"><a-icon type="like" />点赞提醒</a-badge>
        <message-list view="upvote" key="1"> </message-list>
      </a-tab-pane>
      <a-tab-pane key="2">
        <a-badge slot="tab" :dot="comment > 0"><a-icon type="message" />评论消息</a-badge>
        <message-list view="comment" key="2"> </message-list>
      </a-tab-pane>
      <a-tab-pane key="3">
        <a-badge slot="tab" :dot="system > 0"><a-icon type="windows" />系统消息</a-badge>
        <message-list view="system" key="3"> </message-list>
      </a-tab-pane>
    </a-tabs>
  </sp-card>
</template>

<script>
import messageList from './messageList.vue';

export default {
  name: 'messageRemind',
  components: { messageList },
  data() {
    return {
      upvote: 0,
      comment: 0,
      system: 0
    };
  },
  activated() {
    this.refreshCount();
  },
  methods: {
    async refreshCount() {
      const result = await sp.get('api/message_remind/unread_message_count');
      const { upvote, comment, system } = result;
      this.upvote = upvote;
      this.comment = comment;
      this.system = system;
    },
    clearCount(tab) {
      switch (tab) {
        case '1':
          this.upvote = 0;
          break;
        case '2':
          this.comment = 0;
          break;
        case '3':
          this.system = 0;
          break;
        default:
          break;
      }
    }
  }
};
</script>

<style lang="less" scoped>
.message {
  padding: 0 1.5rem 1.5rem 1.5rem !important;
}
</style>
