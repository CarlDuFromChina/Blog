export default {
  state: {
    showComment: false
  },
  getters: {
    getShowComment(state) {
      return state.showComment;
    }
  },
  mutations: {
    updateShowComment(state, data) {
      state.showComment = data;
    }
  }
};
