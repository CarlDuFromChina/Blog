import index from './index/index.js';
import login from './login';
import admin from './admin';

export default [
  {
    path: '/post/:id',
    name: 'post',
    component: () => import('./admin/business/post/postReadonly')
  },
  {
    path: '/readingNote/:id',
    name: 'readingNoteReadonly',
    component: () => import('./admin/business/readingNote/readingNoteReadonly')
  }
].concat(index, login, admin);
