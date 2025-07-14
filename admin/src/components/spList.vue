<template>
  <div :style="{ height: '100%', overflowY: 'auto' }">
    <!-- 按钮组 -->
    <slot name="header">
      <sp-header>
        <div>
          <div style="display: inline-block">
            <slot name="buttons"></slot>
          </div>
          <!-- 按钮组 -->
          <sp-button-list
            style="display: inline-block"
            :buttons="buttons"
            @search-change="handleLoadData"
            @unfold="showMore = true"
            @fold="showMore = false"
          ></sp-button-list>
        </div>
      </sp-header>
      <!-- 展示更多筛选条件 -->
      <div v-if="showMore">
        <slot name="more" />
      </div>
    </slot>
    <!-- 按钮组 -->

    <!-- 表格 -->
    <slot name="body">
      <a-table
        :columns="aColumns"
        :data-source="tableData"
        :loading="loading"
        :pagination="myPagination"
        @change="handleTableChange"
        :row-selection="rowSelection"
      >
        <a :slot="firstColumn" slot-scope="text, record" @click="handleClick(record)">{{ text }}</a>
        <template v-for="item in slotColumns" :slot="item.prop" slot-scope="text">
          <span :key="item.prop" v-if="item.type === 'datetime'">{{ text | moment('YYYY-MM-DD HH:mm') }}</span>
          <div :key="item.prop" v-else>
            <a-tag v-for="(tag, index) in JSON.parse(text)" :key="tag" :color="colors[index % colors.length]">
              {{ tag }}
            </a-tag>
          </div>
        </template>
        <span v-if="operationColumn" slot="action" slot-scope="text, record">
          <template v-for="(action, index) in operationColumn.actions">
            <a-button :size="action.size" :type="action.type || ''" @click="action.method(record)" :key="index" style="margin-right: 5px">{{
              action.name
            }}</a-button>
          </template>
        </span>
      </a-table>
    </slot>
    <!-- 表格 -->

    <!-- 编辑页 -->
    <a-modal v-model="editVisible" :title="editTitle" @ok="save" width="60%" okText="确认" cancelText="取消">
      <slot name="edit">
        <component
          ref="edit"
          v-if="editVisible"
          :is="editComponent"
          :related-attr="relatedAttr"
          @close="editVisible = false"
          @load-data="loadData"
        ></component>
      </slot>
    </a-modal>
    <!-- 编辑页 -->
    <slot />
  </div>
</template>

<script>
import spHeader from './spHeader.vue';

export default {
  components: { spHeader },
  name: 'sp-list',
  props: {
    // 操作按钮
    operations: {
      type: Array,
      default: () => []
    },
    // 控制器
    controllerName: {
      type: String,
      required: true
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
    // 标题点击
    headerClick: {
      type: Function
    },
    // 自定义 API
    customApi: {
      type: String,
      default: ''
    },
    // 高级搜索条件
    searchList: {
      type: Array,
      default: () => []
    },
    // 启用分页
    usePagination: {
      type: Boolean,
      default: true
    },
    // 处理返回数据
    handleDataList: {
      type: Function
    },
    // 启用首列点击
    useHeaderClick: {
      type: Boolean,
      default: true
    },
    // 导出
    exportData: {
      type: Function
    },
    // 自动加载
    autoLoad: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      tableData: [],
      normalOperations: [
        { name: 'new', icon: 'plus', operate: this.createData },
        { name: 'delete', icon: 'delete', operate: this.deleteData },
        { name: 'export', icon: 'export', operate: this.exportCsv },
        { name: 'search' },
        { name: 'more' }
      ],
      keyList: ['title'], // 关键字
      colors: ['pink', 'red', 'orange', 'green', 'cyan', 'blue', 'purple'],
      editVisible: false,
      relatedAttr: null,
      loading: false,
      showMore: false,
      searchValue: '',
      selectionIds: [],
      rowSelection: {
        onChange: (selectedRowKeys, selectedRows) => {
          this.selectionIds = selectedRows.map(item => item.id);
        }
      },
      pagination: {
        current: 1,
        total: 0,
        pageSize: 10,
        showSizeChanger: true,
        showTotal: total => `共有 ${total} 条数据`
      }
    };
  },
  mounted() {
    // 标准表格则加载
    if (!this.$slots.body && this.autoLoad) {
      this.loadData();
    }
  },
  computed: {
    // a-table 列转换
    aColumns() {
      return this.columns.map((item, index) => {
        const column = {
          title: item.label,
          dataIndex: item.prop,
          key: item.prop,
          ellipsis: item.ellipsis
        };
        if (item.width) {
          column.width = item.width;
        }
        // 特殊列和首列需自定义列渲染
        if (item.type === 'datetime' || item.type === 'actions' || item.type === 'tag' || index === 0) {
          let prop = '';
          if (this.keyList.includes(item.prop)) {
            prop = `${item.prop}-0`;
          } else {
            prop = item.prop;
          }
          column.scopedSlots = {
            customRender: prop
          };
        }
        return column;
      });
    },
    // 首列列名
    firstColumn() {
      if (this.columns && this.columns.length > 0 && this.useHeaderClick) {
        if (this.keyList.includes(this.columns[0].prop)) {
          return `${this.columns[0].prop}-0`;
        }
        return this.columns[0].prop;
      }
      return '';
    },
    // 时间类型的列
    slotColumns() {
      return this.columns.filter(item => item.type === 'datetime' || item.type === 'tag');
    },
    // 操作列
    operationColumn() {
      return this.columns.find(item => item.type === 'actions');
    },
    // 操作按钮
    buttons() {
      return this.normalOperations.filter(item => this.operations.includes(item.name));
    },
    myPagination() {
      if (!this.usePagination) {
        return false;
      } else {
        return this.pagination;
      }
    }
  },
  methods: {
    // 分页加载
    handleTableChange(pagination) {
      this.pagination.current = pagination.current;
      this.pagination.pageSize = pagination.pageSize;
      this.loadData();
    },
    // 编辑
    handleClick(row) {
      if (this.headerClick && typeof this.headerClick === 'function') {
        this.headerClick(row);
        return;
      }
      this.relatedAttr = {
        id: row.id
      };
      this.editVisible = true;
    },
    handleLoadData(value) {
      this.pagination.current = 1;
      this.loadData(value);
    },
    // 加载数据
    async loadData(value = '') {
      if (this.searchValue !== value) {
        this.searchValue = value;
      }
      if (this.loading) {
        return;
      }
      this.loading = true;
      let url = `api/${this.controllerName}/search?searchList=${JSON.stringify(
        this.searchList
      )}&orderBy=&pageSize=$pageSize&pageIndex=$pageIndex&searchValue=$searchValue`;
      if (!sp.isNullOrEmpty(this.customApi)) {
        url = this.customApi;
      }
      url = url
        .replace('$pageIndex', this.pagination.current)
        .replace('$pageSize', this.pagination.pageSize)
        .replace('$searchValue', this.searchValue);
      try {
        const resp = await sp.get(url);
        if (resp && resp.DataList) {
          this.tableData = resp.DataList;
          this.$set(this.pagination, 'total', resp.RecordCount);
        } else {
          this.tableData = this.handleDataList ? this.handleDataList(resp) : resp;
          this.$set(this.pagination, 'total', this.tableData.length);
        }
        this.setKey(this.tableData);
      } catch (error) {
        this.$message.error(error);
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 200);
      }
    },
    setKey(table) {
      if (table && table.length > 0) {
        table.forEach((item) => {
          item.key = (item || {}).id || uuid.generate();
          if (item.children && item.children.length > 0) {
            this.setKey(item.children);
          }
        });
      }
    },
    // 编辑保存
    save() {
      if (this.$refs.edit) {
        this.$refs.edit.saveData();
      } else {
        this.editVisible = false;
      }
    },
    // 创建数据
    createData() {
      if (this.headerClick && typeof this.headerClick === 'function') {
        this.headerClick();
        return;
      }
      this.relatedAttr = {};
      this.editVisible = true;
    },
    // 删除数据
    deleteData() {
      if (!this.selectionIds || this.selectionIds.length === 0) {
        this.$message.warning('请选择一项，再进行删除');
        return;
      }
      this.$confirm({
        title: '是否删除',
        content: '此操作将永久删除选择项, 是否继续?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.delete(`api/${this.controllerName}/${this.selectionIds.join(',')}`)
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
    exportCsv() {
      if (this.exportData && typeof this.exportData === 'function') {
        this.exportData();
        return;
      }
      sp.get(`/api/${this.controllerName}/export/csv`);
    }
  }
};
</script>
