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
        { prop: 'status', label: '状态' },
        { prop: 'prev_fire_time', label: '上次运行时间', type: 'datetime' },
        { prop: 'next_fire_time', label: '下次运行时间', type: 'datetime' },
        { prop: 'cron_expression', label: '执行计划' },
        { prop: 'description', label: '描述' },
        {
          prop: 'action',
          label: '操作',
          type: 'actions',
          actions: [
            { name: '运行', size: 'small', method: this.start, type: 'primary' },
            { name: '暂停', size: 'small', method: this.pause, type: 'danger' },
            { name: '继续', size: 'small', method: this.resume }
          ]
        }
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
    },
    pause(row) {
      sp.get(`api/${this.controllerName}/pause?jobName=${row.name}`)
        .then(() => {
          this.$refs.list.loadData();
          this.$message.success('执行成功');
        })
        .catch(error => {
          this.$message.error(error);
          return Promise.reject;
        });
    },
    resume(row) {
      sp.get(`api/${this.controllerName}/resume?jobName=${row.name}`)
        .then(() => {
          this.$refs.list.loadData();
          this.$message.success('执行成功');
        })
        .catch(error => {
          this.$message.error(error);
          return Promise.reject;
        });
    }
  }
};
</script>

<style></style>
