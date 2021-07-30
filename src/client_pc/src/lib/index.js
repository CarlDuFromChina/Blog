import { common, http, uuid } from 'web-core';
import store from '../store';
import axios from 'axios';
import './jigsaw';
import spExtension from './sp';

window.sp = Object.assign({}, common, http, spExtension);
window.uuid = Object.assign({}, uuid);
const serverUrl = localStorage.getItem('server_url');
if (process.env.NODE_ENV === 'development') {
  if (!sp.isNullOrEmpty(serverUrl)) {
    store.commit('updateServerUrl', serverUrl);
    console.info('服务器地址修改为：' + serverUrl);
  } else {
    console.info('你可以通过localStorage添加server_url属性指定请求地址');
  }
}

axios.defaults.timeout = 20000;
axios.defaults.withCredentials = true;
axios.defaults.baseURL = sp.getServerUrl();
axios.interceptors.request.use(async config => {
  await checkToken();
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
  () => {
    return Promise.reject(new Error('服务器开小差了'));
  }
);

// download url
const downloadUrl = url => {
  let iframe = document.createElement('iframe');
  iframe.style.display = 'none';
  iframe.src = url;
  iframe.onload = function() {
    document.body.removeChild(iframe);
  };
  document.body.appendChild(iframe);
};

/**
 * 检查token是否有效
 */
async function checkToken() {
  // 匿名用户访问无需检查token
  if (store.getters.isLoggedIn) {
    // 已登录但是token为空重新登录
    if (store.getters.isEmptyToken()) {
      reLogin();
    }

    // 刷新token过期重新登录
    if (store.getters.isRefreshTokenExpired()) {
      reLogin();
    }

    // 授权token过期重新获取
    if (store.getters.isAccessTokenExpired()) {
      await refreshToken();
    }
  }
}

/**
 * 刷新token
 */
async function refreshToken() {
  store.commit('changeTokenWithRefreshToken'); // 更换成RefreshToken
  const resp = await sp.get('api/AuthUser/RefreshAccessToken');
  if (resp) {
    store.commit('updateAccessToken', resp);
  }
}

/**
 * 重新登录
 */
function reLogin() {
  store.commit('clearAuth');
  store.commit('changeLogin', false); // 修改登录状态
  location.href = '/#/login';
}
