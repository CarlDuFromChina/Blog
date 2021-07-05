import index from './index/index.js';
import login from './login';
import admin from './admin';
import notFound from './404';
import shortUrl from './shortUrl';

export default [
  {
    path: '/blog/:id',
    name: 'blogReadonly',
    component: () => import('./admin/business/blog/blogReadonly')
  },
  {
    path: '/readingNote/:id',
    name: 'readingNoteReadonly',
    component: () => import('./admin/business/readingNote/readingNoteReadonly')
  }
].concat(index, login, admin, notFound, shortUrl);
