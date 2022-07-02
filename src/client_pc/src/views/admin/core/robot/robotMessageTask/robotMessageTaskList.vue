<template>
  <sp-list
    ref="list"
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    :use-pagination="false"
  ></sp-list>
</template>

<script>
import robotMessageTaskEdit from './robotMessageTaskEdit';

export default {
  name: 'robot-message-task-list',
  data() {
    return {
      controllerName: 'robot_message_task',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '任务名' },
        { prop: 'runtime', label: '执行时间' },
        { prop: 'robotid_name', label: '机器人' },
        { prop: 'message_type_name', label: '消息类型' },
        { prop: 'job_state_name', label: '任务状态' },
        { prop: 'created_by_name', label: '创建人' },
        {
          prop: 'action',
          label: '操作',
          type: 'actions',
          actions: [
            { name: '运行', size: 'small', method: this.start, type: '' },
            { name: '暂停', size: 'small', method: this.pause, type: 'danger' },
            { name: '继续', size: 'small', method: this.resume, type: 'primary' }
          ]
        }
      ],
      editComponent: robotMessageTaskEdit
    };
  },
  methods: {
    start(row) {
      this.$confirm({
        title: '提示',
        content: '是否确认运行该作业?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.get(`api/${this.controllerName}/${row.id}/run`).then(() => {
            this.$refs.list.loadData();
            this.$message.success('执行成功');
          });
        },
        onCancel: () => {
          this.$message.info('已取消');
        }
      });
    },
    pause(row) {
      this.$confirm({
        title: '提示',
        content: '是否暂停该作业?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.get(`api/${this.controllerName}/${row.id}/pause`).then(() => {
            this.$refs.list.loadData();
            this.$message.success('执行成功');
          });
        },
        onCancel: () => {
          this.$message.info('已取消');
        }
      });
    },
    resume(row) {
      this.$confirm({
        title: '提示',
        content: '是否继续该作业?',
        okText: '确认',
        cancelText: '取消',
        onOk: () => {
          sp.get(`api/${this.controllerName}/${row.id}/resume`).then(() => {
            this.$refs.list.loadData();
            this.$message.success('执行成功');
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
