<template>
  <div>
    <slot name="header">
      <sp-header v-if="buttons && buttons.length > 0">
        <sp-button-list :buttons="buttons" @search-change="loadData"></sp-button-list>
      </sp-header>
    </slot>
    <slot name="body">
      <a-table :columns="aColumns" :data-source="tableData" :loading="loading" :pagination="pagination" @change="handleTableChange">
        <a :slot="firstColumn" slot-scope="text, record" @click="handleClick(record)">{{ text }}</a>
        <span slot="createdOn" slot-scope="text">{{ text | moment('YYYY-MM-DD HH:mm') }}</span>
        <!-- <template v-for="item in dateColumns">
          <span :slot="item.prop" slot-scope="text" :key="item.prop">
            {{ text | moment('YYYY-MM-DD HH:mm') }}
          </span>
        </template> -->
      </a-table>
    </slot>
    <slot name="edit">
      <a-modal v-model="editVisible" :title="editTitle" @ok="save">
        <component ref="edit" v-if="editVisible" :is="editComponent" :related-attr="relatedAttr"></component>
      </a-modal>
    </slot>
  </div>
</template>

<script>
export default {
  name: 'sp-list',
  props: {
    // 操作按钮
    operations: {
      type: Array,
      default: () => []
    },
    // 控制器
    controllerName: {
      type: String
    },
    // 列
    columns: {
      type: Array,
      default: () => []
    },
    // 编辑页组件
    editComponent: {
      type: Object
    },
    // 编辑页标题
    editTitle: {
      type: String,
      default: '编辑'
    },
    // 是否可以选择列
    allowSelect: {
      type: Boolean,
      default: false
    },
    // 标题点击
    headerClick: {
      type: Function
    },
    // 自定义 API
    customApi: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      tableData: [],
      normalOperations: [
        { name: 'new', icon: 'plus', operate: this.createData },
        { name: 'delete', icon: 'delete', operate: this.deleteData },
        { name: 'search' }
      ],
      pagination: {
        current: 1,
        total: 0,
        pageSize: 5,
        showSizeChanger: true,
        showTotal: total => `共有 ${total} 条数据`
      },
      editVisible: false,
      relatedAttr: null,
      loading: false,
      searchValue: ''
    };
  },
  mounted() {
    if (!this.isLoad) {
      this.loadData();
    }
  },
  computed: {
    aColumns() {
      return this.columns.map((item, index) => {
        const column = {
          title: item.label,
          dataIndex: item.prop
        };
        if (item.type === 'datetime' || index === 0) {
          column.scopedSlots = {
            customRender: item.prop
          };
        }
        return column;
      });
    },
    firstColumn() {
      if (this.columns && this.columns.length > 0) {
        return this.columns[0].prop;
      }
      return '';
    },
    dateColumns() {
      return this.columns.filter(item => item.type === 'datetime');
    },
    buttons() {
      return this.normalOperations.filter(item => this.operations.includes(item.name));
    },
    isLoad() {
      return !!this.$slots.body;
    }
  },
  methods: {
    handleTableChange(pagination) {
      this.pagination.current = pagination.current;
      this.pagination.pageSize = pagination.pageSize;
      this.loadData();
    },
    handleClick(row) {
      this.relatedAttr = {
        id: row.Id
      };
      this.editVisible = true;
    },
    async loadData(value = '') {
      if (this.searchValue !== value) {
        this.searchValue = value;
      }
      if (this.loading) {
        return;
      }
      this.loading = true;
      let url = `api/${this.controllerName}/GetDataList?searchList=&orderBy=&pageSize=${this.pagination.pageSize}&pageIndex=${this.pagination.current}&searchValue=${this.searchValue}`;
      if (!sp.isNullOrEmpty(this.customApi)) {
        url = this.customApi;
      }
      try {
        const resp = await sp.get(url);
        if (resp && resp.DataList) {
          this.tableData = resp.DataList;
          this.pagination.total = resp.RecordCount;
        } else {
          this.tableData = resp;
        }
      } catch (error) {
        this.$message.error(error);
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    save() {
      debugger;
      this.$refs.edit.saveData();
    },
    createData() {
      this.relatedAttr = {};
      this.editVisible = true;
    },
    deleteData() {
      if (!this.selections || this.selections.length === 0) {
        this.$message.warning('请选择一项，再进行删除');
        return;
      }
      this.$confirm({
        title: '是否删除',
        content: '此操作将永久删除该菜单, 是否继续?',
        onOk: () => {
          const ids = this.selections.map(item => {
            return item.Id;
          });
          sp.post(`api/${this.controllerName}/DeleteData`, ids).then(() => {
            this.$message.success('删除成功');
            this.loadData();
          });
        },
        onCancel: () => {
          this.$message.info('已取消删除');
        }
      });
    }
  }
};
</script>

<style lang="less" scoped>
.compute-span {
  color: #409eff;
  text-decoration: none;
}
.el-pagination {
  text-align: center;
  padding-top: 20px;
}
</style>
