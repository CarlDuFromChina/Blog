<template>
  <div>
    <a-row type="flex" v-if="data && data.length > 0">
      <a-col :span="6" v-for="item in data" :key="item.Id">
        <!-- :src="`${baseUrl}/${item.imageSrc}`" -->
        <a-card hoverable style="width: 100%;" @click.native.stop="goReadonly(item)">
          <img slot="cover" alt="example" src="https://gw.alipayobjects.com/zos/rmsportal/JiqGstEfoWAOHiTxclqi.png" />
          <template slot="actions" class="ant-card-actions">
            <a-icon key="delete" type="delete" @click.native.stop="deleteData(item)" />
            <a-icon key="edit" type="edit" @click.native.stop="goEdit(item)" />
            <a-icon key="eye" type="eye" @click.native.stop="goReadonly(item)" />
          </template>
          <a-card-meta :title="item.title" :description="`${item.createdByName} ${$moment(item.modifiedOn).format('YYYY-MM-DD HH:MM')}`">
            <a-avatar slot="avatar" src="https://zos.alipayobjects.com/rmsportal/ODTLcjxAfvqbxHnVXCYX.png" />
          </a-card-meta>
        </a-card>
      </a-col>
      <a-col :span="6" style="100%" v-if="!readonly">
        <a-card style="text-align:center">
          <a-button type="primary" shape="circle" icon="plus" @click="createData" />
        </a-card>
      </a-col>
    </a-row>
    <a-empty v-else style="padding-top:30%" />
  </div>
</template>

<script>
export default {
  name: 'spBlogCard',
  inject: ['getType'],
  props: {
    fetch: { type: Function },
    readonly: {
      type: Boolean,
      default: false
    },
    newTag: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      data: [],
      baseUrl: ''
    };
  },
  created() {
    this.baseUrl = localStorage.getItem('baseUrl');
    // this.loadData();
  },
  computed: {
    buttons() {
      return [{ name: 'new', icon: 'plus', operate: this.createData }];
    }
  },
  methods: {
    isNewBlog(item) {
      return this.$moment().diff(this.$moment(item.createdOn), 'day') < 5;
    },
    loadData() {
      this.$emit('loading');
      this.fetch()
        .then(resp => {
          this.data = resp.DataList;
        })
        .catch(() => {
          this.$message.error('加载出错了');
        })
        .finally(() => {
          this.$emit('loading-close');
        });
    },
    deleteData(item) {
      this.$confirm({
        title: '是否删除',
        content: '此操作将永久删除该博客, 是否继续?',
        ok() {
          sp.post('api/Blog/DeleteData', [item.Id]).then(() => {
            this.$message.success('删除成功');
            this.loadData();
          });
        },
        cancel() {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });
        }
      });
    },
    createData() {
      this.$router.push({
        name: 'blogEdit',
        params: {
          blogType: this.getType()
        }
      });
    },
    goReadonly(row) {
      if (!sp.isNullOrEmpty(row.Id)) {
        this.$router.push({
          name: 'blogReadonly',
          params: { id: row.Id }
        });
      } else {
        this.$message.error('查看失败');
      }
    },
    goEdit(row) {
      if (!sp.isNullOrEmpty(row.Id)) {
        this.$router.push({
          name: 'blogEdit',
          params: {
            id: row.Id
          }
        });
      } else {
        this.$message.error('编辑失败');
      }
    }
  }
};
</script>

<style lang="less" scoped>
/deep/.el-card.is-hover-shadow:hover {
  box-shadow: 0 2px 12px 0 #6750d7;
}
/deep/.ant-col.ant-col-6 {
  padding: 5px;
}
</style>
