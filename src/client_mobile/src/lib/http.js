import axios from 'axios';
import { common, http, uuid } from '@sixpence/web-core';
import spExtension from './sp';

var originHttp = {
  originGet: (url, config) => {
    var instance = axios.create({
      timeout: 5000,
      baseURL: window.origin
    });
    return instance.get(url, config);
  }
};

window.sp = Object.assign({}, common, http, originHttp, spExtension);
window.uuid = Object.assign({}, uuid);

// 开发环境修改后端服务地址
const serverUrl = localStorage.getItem('server_url');
if (process.env.NODE_ENV === 'development') {
  if (!sp.isNullOrEmpty(serverUrl)) {
    axios.defaults.baseURL = serverUrl;
    console.info('服务器地址修改为：' + serverUrl);
  } else {
    axios.defaults.baseURL = 'http://localhost:5000';
    console.warn('你可以通过localStorage添加server_url属性指定请求地址');
  }
}

axios.defaults.timeout = 20000;
axios.defaults.withCredentials = true;
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

function _handleSuccess(res) {
  if (!res) return;

  if (!res.data) {
    return res.data;
  }

  if (res.data.ErrorCode === 0) {
    return res.data.Data || res.data;
  } else if (!sp.isNull(res.data.ErrorCode) && !sp.isNull(res.data.Message)) {
    return Promise.reject(new Error(res.data.Message));
  } else {
    return res.data;
  }
}

function getErrorMessage(error, defaultMessage = 'Oops！') {
  if (typeof error === 'string') {
    return error;
  }

  if (error && error.Message) {
    return error.Message;
  }

  if (error && error.message) {
    return error.message;
  }

  return defaultMessage;
}

function _handleError(error) {
  const { status, data = {} } = error.response || {};
  let errorMessage;
  if (status === 401) {
    errorMessage = getErrorMessage(data, '您没有权限访问该资源');
  } else if (status === 403) {
    errorMessage = '请重新登录';
  } else if (status === 500) {
    errorMessage = getErrorMessage(data, '系统错误，请联系管理员');
  } else if (status === 404) {
    errorMessage = getErrorMessage(data.Message, '未找到资源');
  } else if (data.Message) {
    errorMessage = data.Message;
  } else if (error.message) {
    errorMessage = error.message;
  } else {
    errorMessage = 'Oops!';
  }
  return errorMessage;
}

export function get(url, config) {
  return new Promise(function (resolve, reject) {
    axios
      .get(url, config)
      .then(res => {
        resolve(_handleSuccess(res));
      })
      .catch(err => {
        reject(_handleError(err));
      });
  });
}

export function post(url, data, config) {
  return new Promise(function (resolve, reject) {
    axios
      .post(url, data, config)
      .then(function (res) {
        resolve(_handleSuccess(res));
      })
      .catch(function (err) {
        reject(_handleError(err));
      });
  });
}

export default {
  get,
  post
};
