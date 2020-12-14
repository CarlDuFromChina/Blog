export default [
  {
    path: '/admin/readingNote',
    name: 'readingNoteList',
    component: () => import('./readingNoteList'),
    meta: { title: '阅读笔记' }
  },
  {
    path: '/admin/readingNote/edit/:id?',
    name: 'readingNoteEdit',
    component: () => import('./readingNoteEdit'),
    meta: { title: '阅读笔记 / 编辑' }
  }
];
