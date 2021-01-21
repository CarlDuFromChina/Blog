// eslint-disable-next-line no-extend-native
Object.defineProperty(String.prototype, 'toDownloadUrl', {
  value: function toDownloadUrl() {
    if (sp.isNullOrEmpty(this)) {
      return '';
    }
    return `${sp.getBaseUrl()}api/SysFile/Download?objectId=${this}`;
  },
  writable: false
});
