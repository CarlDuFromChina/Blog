<template>
  <a-comment :avatar="getAvatar(data.created_by)" style="padding: 10px">
    <a slot="author">
      <template v-if="data.name === '名称'">
        {{ data.created_by_name }}
        <span>{{ data.name }}</span>
      </template>
      <template v-else-if="data.name === '回复'">
        {{ data.created_by_name }}
        <span>{{ data.name }}</span>
        {{ data.replyid_name }}
      </template>
      <template v-else>
        {{ data.created_by_name }}
      </template>
    </a>
    <p slot="content" style="background: #f7f8fa">
      {{ data.comment }}
    </p>
    <a-tooltip slot="datetime" :title="data.created_at | moment('YYYY-MM-DD HH:mm:ss')">
      <span>{{ formtDate(data.created_at) }}</span>
    </a-tooltip>
    <template slot="actions">
      <!-- <span key="comment-basic-like" @click="like">
          <a-icon type="like" :theme="action === 'liked' ? 'filled' : 'outlined'" />
          <span style="padding-left: '8px';cursor: 'auto'">点赞</span>
        </span> -->
      <span key="comment-basic-reply-to" @click="clickReply">
        <a-icon type="form" :theme="showReply ? 'filled' : 'outlined'" />
        <span style="padding-left: '8px';cursor: 'auto'">{{ showReply ? '取消回复' : '回复' }}</span>
      </span>
    </template>
    <div v-show="showReply" class="reply">
      <a-textarea :value="value" @change="handleChange"></a-textarea>
      <a-button type="primary" style="margin-top: 8px" size="small" @click="reply">回复</a-button>
    </div>
    <slot />
  </a-comment>
</template>

<script>
export default {
  name: 'sp-comment',
  props: {
    data: {}
  },
  data() {
    return {
      controllerName: 'comments',
      showReply: false,
      value: '',
    };
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn;
    }
  },
  methods: {
    getAvatar(id) {
      if (sp.isNullOrEmpty(id)) {
        var avatar = require('../assets/images/avatar.png');
        return avatar;
      }
      return sp.getAvatar(id);
    },
    formtDate(val) {
      return this.$moment(val).fromNow();
    },
    handleChange(e) {
      this.value = e.target.value;
    },
    clickReply() {
      if (!this.isLoggedIn) {
        this.$emit('login');
        return;
      }
      this.showReply = !this.showReply;
    },
    reply() {
      if (sp.isNullOrEmpty(this.value)) {
        this.$message.warning('请填写回复');
        return;
      }
      const comment = {
        name: '回复',
        comment: this.value,
        objectid: this.data.objectId,
        object_name: this.data.object_name,
        comment_type: 'reply',
        replyid: this.data.created_by,
        replyid_name: this.data.created_by_name,
        parentid: this.data.parentid || this.data.id
      };
      sp.post('api/comments', comment).then(() => {
        this.$message.success('留言成功');
        this.showReply = false;
        this.value = '';
        this.$emit('replied');
      });
    }
  }
};
</script>

<style lang="less" scoped>
.reply {
  background: #f7f8fa;
  padding: 12px;
  display: inline-block;
  text-align: right;
  width: 100%;
}
</style>
