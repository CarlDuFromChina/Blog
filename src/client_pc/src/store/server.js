const DEFAULT_SERVER_URL = process.env.BASE_URL;

export default {
  state: {
    serverUrl: DEFAULT_SERVER_URL
  },
  getters: {
    getServerUrl(state) {
      return (state || {}).serverUrl || DEFAULT_SERVER_URL;
    }
  },
  mutations: {
    updateServerUrl(state, url) {
      state.serverUrl = url || DEFAULT_SERVER_URL;
    }
  }
};
