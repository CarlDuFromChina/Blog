export default {
  state: {
    user: {}
  },
  getters: {
    getUser(state) {
      return state.user;
    },
    getAvatar(state) {
      return sp.getDownloadUrl(state.user.avatar);
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
