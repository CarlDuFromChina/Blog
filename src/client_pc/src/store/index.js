import Vue from 'vue';
import auth from './auth';
import user from './user';
import app from './app';
import persistedState from 'vuex-persistedstate';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
  plugins: [persistedState({ storage: window.localStorage })],
  modules: [auth, user, app]
});
