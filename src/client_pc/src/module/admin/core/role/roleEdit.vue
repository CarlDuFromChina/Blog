<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <a-input v-model="data.name"></a-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="描述">
          <a-input v-model="data.description"></a-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row>
      <a-col :span="12">
        <a-form-model-item label="是否基础角色">
          <a-switch v-model="data.is_basic" disabled />
        </a-form-model-item>
      </a-col>
      <a-col :span="12" v-if="!data.is_basic || data.is_basic == 0">
        <a-form-model-item label="继承角色">
          <sp-select v-model="data.parent_roleid" :options="roles" @change="item => (data.parent_roleidName = item.name)"></sp-select>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-tabs default-active-key="1">
      <a-tab-pane key="1" tab="菜单权限">
        敬请期待
      </a-tab-pane>
      <a-tab-pane key="2" tab="实体权限">
        <entity-privilege :privileges="entityList"></entity-privilege>
      </a-tab-pane>
      <a-tab-pane key="3" tab="按钮权限">
        敬请期待
      </a-tab-pane>
    </a-tabs>
  </a-form-model>
</template>

<script>
import { edit } from '@/mixins';
import entityPrivilege from './components/entityPrivilege';

export default {
  name: 'sysRoleEdit',
  mixins: [edit],
  components: { entityPrivilege },
  data() {
    return {
      controllerName: 'SysRole',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }]
      },
      roles: [],
      data: {
        is_basic: false
      },
      entityList: []
    };
  },
  created() {
    sp.get('api/SysRole/GetBasicRole').then(resp => {
      this.roles = resp;
    });
  },
  methods: {
    loadComplete() {
      this.getEntities();
    },
    getEntities() {
      sp.get(`api/SysRolePrivilege/GetUserPrivileges?roleid=${this.data.Id}`).then(resp => {
        this.entityList = resp;
      });
    }
  }
};
</script>
