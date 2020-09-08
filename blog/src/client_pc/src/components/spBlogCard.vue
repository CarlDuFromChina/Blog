<template>
  <div :infinite-scroll-disabled="busy" :infinite-scroll-distance="10">
    <a-row type="flex" v-if="data && data.length > 0">
      <a-col :span="6" v-for="item in data" :key="item.Id">
        <!-- :src="`${baseUrl}/${item.imageSrc}`" -->
        <a-card hoverable @click.native.stop="goReadonly(item)">
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
      data: []
    };
  },
  mounted() {
    this.loadData();
    this.$bus.$on('load-more', () => this.loadData());
  },
  beforeDestroy() {
    this.$bus.$off('load-more');
  },
  methods: {
    isNewBlog(item) {
      return this.$moment().diff(this.$moment(item.createdOn), 'day') < 5;
    },
    loadData() {
      if (sp.isNullOrEmpty(this.getDataApi)) {
        return;
      }

      if (this.pageSize * this.pageIndex >= this.total && !this.isFirstLoad) {
        return;
      }

      this.busy = true;
      this.$emit('loading');
      sp.get(this.getDataApi.replace('$pageSize', this.pageSize).replace('$pageIndex', this.pageIndex))
        .then(resp => {
          this.data = this.data.concat(resp.DataList);
          this.total = resp.RecordCount;
          this.isFirstLoad = false;
          this.busy = false;
          this.pageIndex += 1;
        })
        .finally(() => {
          this.$emit('loading-close');
        });
    },
    goReadonly(row) {
      this.editVisible = true;
      const read = document.getElementById('blogRead');
      read.innerHTML = row.content;
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
