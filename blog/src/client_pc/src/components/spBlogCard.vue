<template>
  <div :infinite-scroll-disabled="busy" style="width:1200px;margin: 0 auto;">
    <a-row v-if="data && data.length > 0">
      <a-col :span="8" v-for="item in data" :key="item.Id">
        <a-card hoverable @click.native.stop="goReadonly(item)" style="width:380px;margin:10px auto;">
          <div slot="cover" style="height:116px">
            <img alt="example" :src="item.first_picture" />
            <span style="display: inline-block;padding-left: 10px;max-width: calc(100% - 216px);height:100%">
              <div style="font-size: 16px;font-weight: 500;">{{ item.name }}</div>
              <div>{{ item.author }}</div>
              <div>{{ item.modifiedOn | moment('YYYY-MM-DD HH:MM') }}</div>
            </span>
          </div>
        </a-card>
      </a-col>
    </a-row>
    <a-empty v-else style="padding-top:30%" />
    <a-modal title="博客" v-model="editVisible" @ok="editVisible = false" width="80%">
      <div id="blogRead"></div>
    </a-modal>
  </div>
</template>

<script>
import infiniteScroll from 'vue-infinite-scroll';
import { pagination } from 'sixpence.platform.pc.vue';

export default {
  name: 'spBlogCard',
  directives: { infiniteScroll },
  mixins: [pagination],
  props: {
    getDataApi: {
      type: String,
      default: ''
    },
    newTag: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      isFirstLoad: true,
      busy: false,
      editVisible: false,
      data: [],
      pageSize: 15,
      loading: false
    };
  },
  mounted() {
    this.loadData();
  },
  beforeDestroy() {
    this.$bus.$emit('reset');
  },
  methods: {
    isNewBlog(item) {
      return this.$moment().diff(this.$moment(item.createdOn), 'day') < 5;
    },
    loadData() {
      if (this.loading) {
        this.$bus.$emit('loading-finish');
        return;
      }
      this.loading = true;

      if (sp.isNullOrEmpty(this.getDataApi)) {
        this.$bus.$emit('loading-finish');
        this.$bus.$emit('loaded-all');
        return;
      }

      if (this.pageSize * this.pageIndex >= this.total && !this.isFirstLoad) {
        this.$bus.$emit('loading-finish');
        this.$bus.$emit('loaded-all');
        return;
      }

      this.busy = true;
      this.$emit('loading');
      if (!this.isFirstLoad) {
        this.pageIndex += 1;
      }
      sp.get(this.getDataApi.replace('$pageSize', this.pageSize).replace('$pageIndex', this.pageIndex))
        .then(resp => {
          this.data = this.data.concat(resp.DataList);
          this.total = resp.RecordCount;
          this.isFirstLoad = false;
          this.busy = false;
        })
        .finally(() => {
          this.loading = false;
          this.$emit('loading-close');
          this.$bus.$emit('loading-finish');
        });
    },
    goReadonly(row) {
      this.editVisible = true;
      setTimeout(() => {
        const read = document.getElementById('blogRead');
        read.innerHTML = row.content;
      }, 40);
    }
  }
};
</script>

<style lang="less" scoped>
/deep/.ant-col.ant-col-6 {
  padding: 5px;
}
.ant-card-cover img {
  float: left;
  width: 206px;
  height: 116px;
}
.demo-infinite-container {
  border: 1px solid #e8e8e8;
  border-radius: 4px;
  overflow: auto;
  padding: 8px 24px;
  height: 300px;
}
</style>
