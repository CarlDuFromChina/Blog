export default [
  {
    path: '/admin/editor/post/:id?',
    name: 'postEdit',
    component: () => import('./postEdit'),
    meta: { title: '博客编辑' }
  },
  {
    path: '/admin/editor/draft/:draftId?',
    name: 'draftEdit',
    component: () => import('./postEdit'),
    meta: { title: '草稿编辑' }
  },
]