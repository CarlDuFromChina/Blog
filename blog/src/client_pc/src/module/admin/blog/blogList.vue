<template>
  <!-- <sp-blog-card ref="blog" :fetch="fetchData" v-infinite-scroll="load" style="overflow:auto;"></sp-blog-card> -->
  <a-list :loading="loading" item-layout="horizontal" :pagination="pagination" :data-source="data" rowKey="Id">
    <a-list-item slot="renderItem" slot-scope="item">
      <a slot="actions" @click="goEdit(item)">编辑</a>
      <a slot="actions" @click="goReadonly(item)">查看</a>
      <a-list-item-meta :description="item.createdByName">
        <a slot="title" href="javascript:void(0);" @click="goReadonly(item)">{{ item.title }}</a>
        <a-avatar slot="avatar" src="http://www.dumiaoxin.top:8002/temp/1B715131BA7631E818D1713D3E6766E541717022.png" />
      </a-list-item-meta>
    </a-list-item>
  </a-list>
</template>

<script>
export default {
  name: 'blogList',
  provide() {
    return {
      getType: this.getType
    };
  },
  data() {
    return {
      loading: false,
      data: [],
      currentIndex: 0,
      pagination: {
        onChange: async page => {
          this.data = this.data.concat(await this.fetchData(10, page));
        },
        pageSize: 10,
        total: 20
      }
    };
  },
  async created() {
    this.data = await this.fetchData(10, 1);
  },
  methods: {
    getType() {
      return this.$route.params.type;
    },
    fetchData(pageSize, pageIndex) {
      if (this.currentIndex < pageIndex) {
        this.currentIndex = pageIndex;
      } else {
        return [];
      }
      const searchList = [{ Name: 'blog_type', Value: this.$route.params.type }];
      return sp
        .get(`api/blog/GetDataList?orderBy=&pageSize=${pageSize}&pageIndex=${pageIndex}&searchList=${JSON.stringify(searchList)}`)
        .then(resp => {
          this.pagination.total = resp.RecordCount;
          return resp.DataList;
        })
        .catch(() => this.$message.error('加载出错了'));
    },
    goReadonly(item) {
      this.$router.push({
        name: 'blogReadonly',
        params: { id: item.Id }
      });
    },
    goEdit(item) {
      this.$router.push({
        name: 'blogEdit',
        params: { id: item.Id }
      });
    }
  }
};
</script>

<style></style>
