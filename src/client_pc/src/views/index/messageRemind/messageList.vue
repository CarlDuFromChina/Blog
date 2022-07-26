<template>
  <div>
    <a-list item-layout="horizontal" size="large" :data-source="listData">
      <a-list-item slot="renderItem" slot-scope="item">
        <a-list-item-meta v-if="view === 'upvote'">
          <span slot="title"
            >{{ item.created_by_name }} 赞了你的文章 <a href="www.baidu.com">{{ item.data.object_id_name }}</a></span
          >
          <a-avatar slot="avatar" :src="item.avatar" />
        </a-list-item-meta>
        <a-list-item-meta v-else-if="view === 'comment'" :description="item.data.comment">
          <span slot="title"
            >{{ item.created_by_name }} 评论了你的文章
            <a :href="`/post/${item.data.objectId}`" target="_blank">《{{ item.data.object_title }}》</a></span
          >
          <a-avatar slot="avatar" :src="item.avatar" />
        </a-list-item-meta>
        <a slot="actions" @click="openLink(item)">查看</a>
      </a-list-item>
    </a-list>
    <a-spin :spinning="loading" :delay="100" style="width: 100%; padding: 10px 0; text-align: center">
      <span v-if="isLoadedAll">到底了....</span>
    </a-spin>
  </div>
</template>

<script>
import { pagination } from '@/mixins';
const view = ['comment', 'upvote', 'system'];
export default {
  name: 'messageList',
  mixins: [pagination],
  props: {
    view: {
      type: String,
      validator: function(val) {
        return view.includes(val);
      }
    }
  },
  data() {
    return {
      baseUrl: sp.getServerUrl(),
      listData: [],
      loading: false,
      isLoadedAll: false,
      searchValue: ''
    };
  },
  created() {
    if (this.view === 'comment') {
      this.viewId = '9E778EBC-9961-4CF7-B352-36DF30F33735';
    } else if (this.view === 'upvote') {
      this.viewId = 'FBAF583B-4B25-477B-B5DC-C5D110976A9A';
    } else if (this.view === 'system') {
      this.viewId = 'F7A3E0A9-4951-4EA7-A486-5B35056AC17A';
    }
    this.fetchData();
    this.$bus.$on('load-more', () => {
      if (this.isLoadedAll) {
        return;
      }
      if (this.loading) {
        return;
      }
      this.loading = true;
      setTimeout(() => {
        this.fetchData();
      }, 500);
    });
  },
  methods: {
    fetchData() {
      sp.get(
        `api/message_remind/search?orderBy=created_at desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=&viewId=${this.viewId}&searchValue=${this.searchValue}`
      ).then(resp => {
        this.total = resp.RecordCount;
        resp.DataList.forEach(item => {
          item.avatar = sp.getAvatar(item.created_by);
          item.data = JSON.parse(item.content);
        });
        this.listData = this.listData.concat(resp.DataList);
        this.isLoadedAll = this.pageSize * this.pageIndex >= this.total;
        this.pageIndex++;
      })
      .finally(() => {
        this.loading = false;
      })
    },
    openLink(item) {
      window.open(`/post/${item.data.objectId}`, '_blank');
    }
  }
};
</script>

<style lang="less" scoped>
/deep/ .ant-list-item-meta-description {
  margin-bottom: 10px;
  padding: 10px;
  background: #fafbfc;
  border-radius: 3px;
  border: 1px solid #f1f1f2;
}
</style>
