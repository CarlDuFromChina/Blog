
import index from './index/index.js';

export default [
  {
    path: '/blogReadonly/:id',
    name: 'blogReadonly',
    component: () => import('./admin/blog/blogReadonly')
  }
].concat(index);
