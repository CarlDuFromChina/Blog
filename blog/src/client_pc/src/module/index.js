import index from './index/index.js';

export default [
  {
    path: '/blog/:id',
    name: 'blogReadonly',
    component: () => import('./admin/blog/blogReadonly')
  }
].concat(index);
