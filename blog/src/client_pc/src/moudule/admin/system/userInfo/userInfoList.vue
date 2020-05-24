<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns" :editComponent="editComponent" :allow-select="true"></sp-list>
</template>

<script>
import userInfoEdit from './userInfoEdit';

export default {
  name: 'userInfoList',
  data() {
    return {
      controllerName: 'UserInfo',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'code', label: '编码' },
        { prop: 'genderName', label: '性别' },
        { prop: 'realname', label: '真实姓名' },
        { prop: 'mailbox', label: '邮箱' },
        { prop: 'cellphone', label: '手机号码' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ],
      editComponent: userInfoEdit
    };
  }
};
</script>

<style></style>
