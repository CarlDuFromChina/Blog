<template>
  <a-tabs default-active-key="1">
    <a-tab-pane key="1" tab="基本信息">
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
          <a-col :span="12" v-if="!data.is_basic">
            <a-form-model-item label="继承角色">
              <sp-select v-model="data.parent_roleid" :options="roles" @change="item => (data.parent_roleid_name = item.name)"></sp-select>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </a-tab-pane>
    <a-tab-pane v-if="pageState === 'edit'" key="2" tab="菜单权限">
      <menu-privilege :privileges="menuList"></menu-privilege>
    </a-tab-pane>
    <a-tab-pane v-if="pageState === 'edit'" key="3" tab="实体权限">
      <entity-privilege :privileges="entityList"></entity-privilege>
    </a-tab-pane>
    <a-tab-pane v-if="pageState === 'edit'" key="4" tab="按钮权限">
      敬请期待
    </a-tab-pane>
  </a-tabs>
</template>

<script>
import { edit } from '@/mixins';
import entityPrivilege from './components/entityPrivilege';
import menuPrivilege from './components/menuPrivilege';

export default {
  name: 'sysRoleEdit',
  mixins: [edit],
  components: { entityPrivilege, menuPrivilege },
  data() {
    return {
      controllerName: 'sys_role',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }]
      },
      roles: [],
      data: {
        is_basic: false
      },
      entityList: [],
      menuList: []
    };
  },
  created() {
    sp.get('api/sys_role/basic_role_options').then(resp => {
      this.roles = resp;
    });
  },
  methods: {
    loadComplete() {
      this.getEntities();
    },
    async getEntities() {
      this.entityList = await sp.get(`api/sys_role_privilege/${this.data.id}/0`);
      this.menuList = await sp.get(`api/sys_role_privilege/${this.data.id}/1`);
    }
  }
};
</script>
