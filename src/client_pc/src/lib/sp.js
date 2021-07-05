import store from '../store';

function getServerUrl() {
  let url = store.getters.getServerUrl;
  return url.charAt(url.length - 1) === '/' ? url : url + '/';
}

function getUserId() {
  return store.getters.getUserId;
}

export default {
  getServerUrl,
  getUserId
};
