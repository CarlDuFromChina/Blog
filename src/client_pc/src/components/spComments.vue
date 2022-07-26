<template>
  <div>
    <a-comment v-if="!disabled">
      <a-avatar slot="avatar" :src="getAvatar()" alt="Han Solo" />
      <div slot="content">
        <a-form-item>
          <a-textarea ref="commentInput" placeholder="请输入评论" :rows="4" :value="value" @change="handleChange" @click="showLogin" />
        </a-form-item>
        <a-form-item>
          <a-button html-type="submit" :loading="submitting" type="primary" @click="handleSubmit" :disabled="!isLoggedIn">
            发表评论
          </a-button>
        </a-form-item>
      </div>
    </a-comment>
    <a-list
      v-if="comments.length"
      :data-source="comments"
      :header="`${comments.length} ${comments.length > 1 ? 'replies' : 'reply'}`"
      item-layout="horizontal"
    >
      <a-list-item slot="renderItem" slot-scope="item">
        <sp-comment :data="item" @replied="getDataList" @login="showLogin">
          <sp-comment v-for="item2 in item.Comments" :key="item2.id" :data="item2" @replied="getDataList" @login="showLogin"></sp-comment>
        </sp-comment>
      </a-list-item>
    </a-list>
    <sp-login ref="login" @closed="closed"></sp-login>
  </div>
</template>
<script>
import spComment from './spComment.vue';

export default {
  name: 'sp-comments',
  components: { spComment },
  props: {
    objectId: {
      type: String,
      default: '',
      required: true
    },
    objectName: {
      type: String,
      default: '',
      required: true
    },
    data: {
      type: Object
    },
    disabled: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      controllerName: 'comments',
      comments: [],
      submitting: false,
      value: ''
    };
  },
  created() {
    this.getDataList();
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn;
    }
  },
  methods: {
    showLogin() {
      if (!this.isLoggedIn) {
        this.$refs.login.editVisible = true;
      }
    },
    closed() {
      if (!this.isLoggedIn) {
        setTimeout(() => {
          this.$refs.commentInput.blur();
        }, 0);
      }
    },
    getDataList() {
      const searchList = [{ Name: 'objectid', Value: this.objectId, Type: 0 }];
      sp.get(`api/${this.controllerName}/search?searchList=${JSON.stringify(searchList)}&orderBy=created_at desc`).then(resp => {
        this.comments = resp.DataList;
      });
    },
    handleSubmit() {
      if (!this.value) {
        this.$message.warning('请填写评论');
        return;
      }

      this.submitting = true;

      setTimeout(() => {
        this.submitting = false;
        const comment = {
          name: '评论',
          comment: this.value,
          comment_type: 'comment',
          object_ownerid: this.data.created_by,
          object_ownerid_name: this.data.created_by_name,
          objectid: this.objectId,
          object_name: this.objectName,
          object_title: this.data.title
        };
        sp.post('api/comments', comment).then(() => {
          this.getDataList();
          this.$message.success('留言成功');
        });
        this.value = '';
      }, 1000);
    },
    handleChange(e) {
      this.value = e.target.value;
    },
    formtDate(val) {
      return this.$moment(val).fromNow();
    },
    getAvatar() {
      return sp.getAvatar();
    }
  }
};
</script>

<style lang="less" scoped>
.ant-list {
  /deep/ .ant-comment {
    width: 100%;
    background: #fff;
  }
}

/deep/ .ant-list-item {
  padding: 0;
}
</style>
