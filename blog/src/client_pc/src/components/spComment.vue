<template>
  <div>
    <a-list
      v-if="comments.length"
      :data-source="comments"
      :header="`${comments.length} ${comments.length > 1 ? 'replies' : 'reply'}`"
      item-layout="horizontal"
    >
      <a-list-item slot="renderItem" slot-scope="item">
        <a-comment :author="item.name" :avatar="item.avatar" :content="item.comment" :datetime="item.createdOn | moment('YYYY-MM-DD HH:mm')" />
      </a-list-item>
    </a-list>
    <a-comment>
      <a-avatar slot="avatar" src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" alt="Han Solo" />
      <div slot="content">
        <a-form-item>
          <a-textarea :rows="4" :value="value" @change="handleChange" />
        </a-form-item>
        <a-form-item>
          <a-button html-type="submit" :loading="submitting" type="primary" @click="handleSubmit">
            提交留言
          </a-button>
        </a-form-item>
      </div>
    </a-comment>
  </div>
</template>
<script>
export default {
  name: 'sp-comment',
  props: {
    objectId: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      controllerName: 'Comments',
      comments: [],
      submitting: false,
      value: ''
    };
  },
  created() {
    this.getDataList();
  },
  methods: {
    getDataList() {
      sp.get(`api/${this.controllerName}/GetDataList?searchList&orderBy=createdOn desc`).then(resp => {
        this.comments = resp;
      });
    },
    handleSubmit() {
      if (!this.value) {
        return;
      }

      this.submitting = true;

      setTimeout(() => {
        this.submitting = false;
        const comment = {
          Id: sp.newUUID(),
          name: '游客',
          avatar: 'https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png',
          comment: this.value,
          objectid: this.objectId
        };
        sp.post('api/Comments/CreateData', comment).then(resp => {
          this.getDataList();
        });
        this.value = '';
      }, 1000);
    },
    handleChange(e) {
      this.value = e.target.value;
    }
  }
};
</script>
