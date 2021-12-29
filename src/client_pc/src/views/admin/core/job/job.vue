<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :customApi="customApi"
    :columns="columns"
    :use-pagination="false"
    :use-header-click="false"
  ></sp-list>
</template>

<script>
export default {
  name: 'job',
  data() {
    return {
      controllerName: 'job',
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'lastRunTime', label: '上次运行时间', type: 'datetime' },
        { prop: 'nextRunTime', label: '下次运行时间', type: 'datetime' },
        { prop: 'runTime', label: '执行计划' },
        { prop: 'description', label: '描述' },
        { prop: 'action', label: '操作', type: 'actions', actions: [{ name: '运行', size: 'small', method: this.start }] }
      ]
    };
  },
  computed: {
    customApi() {
      return `api/${this.controllerName}/GetDataList`;
    }
  },
  methods: {
    start(row) {
      this.$confirm({
        title: '提示',
        content: '是否确认运行该作业?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.get(`api/${this.controllerName}/RunOnceNow?name=${row.name}`)
            .then(() => {
              this.$refs.list.loadData();
              this.$message.success('执行成功');
            })
            .catch(error => {
              this.$message.error(error);
              return Promise.reject;
            });
        },
        onCancel: () => {
          this.$message.info('已取消');
        }
      });
    }
  }
};
</script>

<style></style>
