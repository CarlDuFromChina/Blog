import store from '../store';

function getServerUrl() {
  let url = store.getters.getServerUrl;
  return url.charAt(url.length - 1) === '/' ? url : url + '/';
}

function getUserId() {
  return store.getters.getUserId;
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

export default {
  getServerUrl,
  getUserId,
  getDownloadUrl
};
