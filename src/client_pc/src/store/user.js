export default {
  state: {
    user: {}
  },
  getters: {
    getUser(state) {
      return state.user;
    },
    getAvatar(state) {
      return `${sp.getServerUrl()}api/SysFile/Download?objectId=${state.user.avatar}`;
    }
  },
  mutations: {
    updateUser(state, data) {
      state.user = data;
    },
    clearUser(state) {
      state.user = {};
    }
  }
};
