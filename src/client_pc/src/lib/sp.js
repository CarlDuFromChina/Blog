import store from '../store';
import axios from 'axios';

function getServerUrl() {
  let url = axios.defaults.baseURL || window.origin;
  return url.charAt(url.length - 1) === '/' ? url : url + '/';
}

function getDownloadUrl(value, isUrl = true) {
  if (sp.isNullOrEmpty(value)) {
    return '';
  }
  const url = isUrl ? value : `/api/SysFile/Download?objectId=${value}`;
  if (url.charAt(0) === '/') {
    return `${getServerUrl().trimEnd('/')}${url}`;
  }
  return `${getServerUrl()}${url}`;
}

function getAvatar(id) {
  if (sp.isNullOrEmpty(id)) {
    return `${getServerUrl()}api/System/GetAvatar?id=${getUserId()}`;
  }
  return `${getServerUrl()}api/System/GetAvatar?id=${id}`;
}

function getUserId() {
  return store.getters.getUserId;
}

export default {
  getServerUrl,
  getDownloadUrl,
  getAvatar,
  getUserId
};
