import index from './index/index.js';

export default [
  {
    path: '/blog/:id',
    name: 'blogReadonly',
    component: () => import('./admin/blog/blogReadonly')
  },
  {
    path: '/readingNote/:id',
    name: 'readingNoteReadonly',
    component: () => import('./admin/readingNote/readingNoteReadonly')
  }
].concat(index);
