<template>
  <sp-list ref="list" :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent">
    <template slot="buttons">
      <a-tooltip placement="topLeft" title="锁定用户，无法登录">
        <a-button icon="lock" @click="lock" style="margin-right:10px;"></a-button>
      </a-tooltip>
      <a-tooltip placement="topLeft" title="解锁用户">
        <a-button icon="unlock" @click="unlock" style="margin-right:10px;"></a-button>
      </a-tooltip>
    </template>
  </sp-list>
</template>

<script>
import userInfoEdit from './userInfoEdit';

export default {
  name: 'userInfoList',
  data() {
    return {
      controllerName: 'user_info',
      operations: ['new', 'delete', 'search', 'export'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'gender_name', label: '性别' },
        { prop: 'realname', label: '真实姓名' },
        { prop: 'mailbox', label: '邮箱' },
        { prop: 'cellphone', label: '手机号码' },
        { prop: 'is_lock_name', label: '是否锁定' },
        { prop: 'created_at', label: '创建日期', type: 'datetime' }
      ],
      editComponent: userInfoEdit
    };
  },
  computed: {
    selectionIds() {
      return this.$refs.list.selectionIds;
    }
  },
  methods: {
    lock() {
      if (!this.selectionIds || this.selectionIds.length === 0) {
        this.$message.warning('请选择一项，再进行锁定');
        return;
      }
      if (!this.selectionIds || this.selectionIds.length > 1) {
        this.$message.warning('至多选择一个用户进行锁定');
        return;
      }

      sp.get(`api/auth_user/${this.selectionIds[0]}/lock`)
        .then(() => {
          this.$message.success('锁定成功');
        })
        .catch(error => {
          this.$message.error(error);
        });
    },
    unlock() {
      if (!this.selectionIds || this.selectionIds.length === 0) {
        this.$message.warning('请选择一项，再进行解锁');
        return;
      }
      if (!this.selectionIds || this.selectionIds.length > 1) {
        this.$message.warning('至多选择一个用户进行解锁');
        return;
      }

      sp.get(`api/auth_user/${this.selectionIds[0]}/lock`)
        .then(() => {
          this.$message.success('解锁成功');
        })
        .catch(error => {
          this.$message.error(error);
        });
    }
  }
};
</script>

<style></style>
