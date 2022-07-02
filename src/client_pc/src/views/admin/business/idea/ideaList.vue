<template>
  <div v-infinite-scroll="handleInfiniteOnLoad" class="list" :infinite-scroll-disabled="loading" :infinite-scroll-distance="10">
    <sp-header>
      <sp-button-list
        style="display: inline-block;align-items: center;"
        :buttons="[{ name: 'new', icon: 'plus', operate: this.create }]"
      ></sp-button-list>
    </sp-header>
    <a-list :data-source="data">
      <a-list-item slot="renderItem" slot-scope="item">
        <a-list-item-meta>
          <div slot="title">
            <div style="font-size: 14px">{{ item.created_by_name }}</div>
            <div style="font-size: 13px;">{{ item.created_at }}</div>
          </div>
          <a-avatar slot="avatar" :src="item.avatar" />
          <div slot="description" v-html="item.content"></div>
        </a-list-item-meta>
        <div style="padding-left: 20px">
          <a-button type="primary" icon="edit" @click="edit(item.id)"></a-button>
          <a-button type="danger" icon="delete" @click="deleteData(item.id)"></a-button>
        </div>
      </a-list-item>
    </a-list>
    <a-spin :spinning="loading" :delay="100" style="width: 100%; padding: 10px 0; text-align: center">
      <span v-if="isLoadedAll">到底了....</span>
    </a-spin>
    <a-modal v-model="editVisible" title="编辑" @ok="save" @cancel="relatedAttr = null" width="60%" okText="确认" cancelText="取消">
      <idea-edit ref="edit" v-if="editVisible" @close="editVisible = false" @load-data="loadData" :relatedAttr="relatedAttr"></idea-edit>
    </a-modal>
  </div>
</template>
<script>
import infiniteScroll from 'vue-infinite-scroll';
import { pagination } from '@/mixins';
import ideaEdit from './ideaEdit.vue';

export default {
  directives: { infiniteScroll },
  components: { ideaEdit },
  mixins: [pagination],
  data() {
    return {
      data: [],
      loading: false,
      isLoadedAll: false,
      editVisible: false,
      relatedAttr: null
    };
  },
  beforeMount() {
    this.fetchData(res => {
      this.data = res;
    });
  },
  methods: {
    loadData() {
      this.pageIndex = 1;
      this.fetchData(res => (this.data = res));
    },
    create() {
      this.editVisible = true;
    },
    save() {
      if (this.$refs.edit) {
        this.$refs.edit.saveData();
      } else {
        this.editVisible = false;
      }
    },
    edit(id) {
      this.editVisible = true;
      this.relatedAttr = { id };
    },
    deleteData(id) {
      this.$confirm({
        title: '是否删除',
        content: '此操作将永久删除选择项, 是否继续?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.delete(`api/idea/${id}`)
            .then(() => {
              this.$message.success('删除成功');
              this.loadData();
            })
            .catch(error => {
              this.$message.error(error);
            });
        },
        onCancel: () => {
          this.$message.info('已取消删除');
        }
      });
    },
    fetchData(callback) {
      sp.get(`api/idea/search?orderBy=created_at desc&pageSize=${this.pageSize}&pageIndex=${this.pageIndex}&searchList=&searchValue=`).then(
        resp => {
          const dataList = resp.DataList.map(item => {
            item.avatar = `${sp.getServerUrl()}api/system/avatar/${item.created_by}`;
            return item;
          });
          this.total = resp.RecordCount;
          callback(dataList);
          this.pageIndex++;
        }
      );
    },
    handleInfiniteOnLoad() {
      const data = this.data;
      this.loading = true;
      if (this.pageIndex !== 1 && this.pageSize * this.pageIndex >= this.total) {
        this.isLoadedAll = true;
        this.loading = false;
        return;
      }
      this.fetchData(res => {
        this.data = data.concat(res);
        this.loading = false;
      });
    }
  }
};
</script>
<style>
.list {
  border: 1px solid #e8e8e8;
  border-radius: 4px;
  overflow: auto;
  padding: 8px 24px;
  height: 100%;
  max-height: 100%;
}

.demo-loading-container {
  position: absolute;
  bottom: 40px;
  width: 100%;
  text-align: center;
}
</style>
