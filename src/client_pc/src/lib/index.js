import { common, http, uuid } from 'web-core';
import store from '../store';
import axios from 'axios';
import './jigsaw';
import spExtension from './sp';

window.sp = Object.assign({}, common, http, spExtension);
window.uuid = Object.assign({}, uuid);

axios.defaults.timeout = 20000;
axios.defaults.withCredentials = true;
axios.defaults.baseURL = sp.getServerUrl();
axios.interceptors.request.use(config => {
  config.headers.Authorization = `Bearer ${store.getters.getToken}`;
  return config;
});
axios.interceptors.response.use(
  response => {
    // 处理excel文件
    if (response.headers && response.headers['content-type'] === 'application/octet-stream') {
      downloadUrl(`${sp.getServerUrl()}${response.config.url}`);
      return Promise.resolve(true);
    }
    return Promise.resolve(response);
  },
  error => {
    if (error && error.response && error.response.status) {
      switch (error.response.status) {
        case 403:
          location.href = '/#/login';
          break;
        default:
          break;
      }
      return Promise.reject(error);
    }
    return Promise.reject(new Error('服务器开小差了'));
  }
);

// download url
const downloadUrl = url => {
  let iframe = document.createElement('iframe');
  iframe.style.display = 'none';
  iframe.src = url;
  iframe.onload = function () {
    document.body.removeChild(iframe);
  };
  document.body.appendChild(iframe);
};
