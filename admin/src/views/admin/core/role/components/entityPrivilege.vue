<template>
  <div>
    <a-row v-for="(item, index) in privileges" :key="index">
      <a-col :span="24">
        <a-checkbox-group @change="value => onChange(value, item, index)" :default-value="values[index]" style="width: 100%">
          <a-row>
            <a-col :span="6">
              {{ item.objectid_name }}
            </a-col>
            <a-col :span="6">
              <a-checkbox :value="1">
                读
              </a-checkbox>
            </a-col>
            <a-col :span="6">
              <a-checkbox :value="2">
                写
              </a-checkbox>
            </a-col>
            <a-col :span="6">
              <a-checkbox :value="4">
                删
              </a-checkbox>
            </a-col>
          </a-row>
        </a-checkbox-group>
      </a-col>
    </a-row>
  </div>
</template>

<script>
export default {
  name: 'entity-privilege',
  props: {
    privileges: {
      type: Array,
      default: () => []
    }
  },
  data() {
    return {
      operationTypes: [1, 2, 4]
    };
  },
  computed: {
    values() {
      return this.privileges.map(item => {
        return this.operationTypes.filter(operation => (item.privilege & operation) === operation);
      });
    }
  },
  methods: {
    onChange(value, item, index) {
      this.values[index] = value;
      item.privilege = this.values[index].reduce((pre, cur) => pre + cur, 0);
      this.saveData(item);
    },
    saveData(data) {
      sp.put(`api/sys_role_privilege`, data)
        .then(() => {
          this.$message.success('修改成功');
        })
        .catch(error => {
          this.$message.error(error);
        });
    }
  }
};
</script>
