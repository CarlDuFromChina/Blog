import store from '../store';
import axios from 'axios';

function getServerUrl() {
  let url = axios.defaults.baseURL || window.origin;
  return url.charAt(url.length - 1) === '/' ? url : url + '/';
}

function getUserId() {
  return store.getters.getUserId;
}

function getDownloadUrl(value, isUrl = true) {
  if (sp.isNullOrEmpty(value)) {
    return '';
  }
  const url = isUrl ? value : `/api/sys_file/Download?objectId=${value}`;
  if (url.charAt(0) === '/') {
    return `${getServerUrl().trimEnd('/')}${url}`;
  }
  return `${getServerUrl()}${url}`;
}

export default {
  getServerUrl,
  getUserId,
  getDownloadUrl
};
