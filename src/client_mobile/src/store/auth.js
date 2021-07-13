export default {
  state: {
    isLogin: false,
    auth: {
      token: '',
      userId: ''
    }
  },
  getters: {
    isLoggedIn(state) {
      return !!state.isLogin;
    },
    getToken(state) {
      const { token = '' } = state.auth;
      return token;
    },
    getUserId(state) {
      const { userId = '' } = state.auth;
      return userId;
    }
  },
  mutations: {
    changeLogin(state, data) {
      state.isLogin = data;
    },
    updateAuth(state, data) {
      const { token = '', userId = '' } = data;
      state.auth.token = token;
      state.auth.userId = userId;
    },
    clearAuth(state) {
      state.auth.token = '';
      state.auth.userId = '';
    }
  }
};
