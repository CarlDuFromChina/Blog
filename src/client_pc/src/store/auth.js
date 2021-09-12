export default {
  state: {
    isLogin: false,
    userId: '',
    userName: '',
    token: {} // { RefreshToken, AccessToken }
  },
  getters: {
    isLoggedIn(state) {
      return !!state.isLogin;
    },
    getToken(state) {
      return (state.token.AccessToken || {}).TokenContent;
    },
    getUserId(state) {
      return state.userId;
    },
    getUserName(state) {
      return state.userName;
    },
    isAccessTokenExpired: (state) => () => {
      const { AccessToken } = state.token;
      const t1 = new Date(AccessToken.Expires);
      return t1 < new Date();
    },
    isRefreshTokenExpired: (state) => () => {
      const { RefreshToken } = state.token;
      const t1 = new Date(RefreshToken.Expires);
      return t1 < new Date();
    },
    isEmptyToken: (state) => () => {
      return sp.isNullOrEmpty(state.token);
    }
  },
  mutations: {
    changeLogin(state, data) {
      state.isLogin = data;
    },
    updateAuth(state, data) {
      state.token = data.token;
      state.userId = data.userId;
      state.userName = data.userName;
    },
    changeTokenWithRefreshToken(state) {
      state.token.AccessToken = state.token.RefreshToken;
    },
    clearAuth(state) {
      state.token = {};
      state.userId = '';
    },
    updateAccessToken(state, data) {
      state.token.AccessToken = data;
    }
  }
};
