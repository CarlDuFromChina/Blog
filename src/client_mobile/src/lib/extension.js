import http from './http';

Object.assign(window.sp, http);

// eslint-disable-next-line no-extend-native
Object.defineProperty(String.prototype, 'toDownloadUrl', {
  value: function toDownloadUrl() {
    if (sp.isNullOrEmpty(this)) {
      return '';
    }
    return `${sp.getServerUrl()}api/SysFile/Download?objectId=${this}`;
  },
  writable: false
});
