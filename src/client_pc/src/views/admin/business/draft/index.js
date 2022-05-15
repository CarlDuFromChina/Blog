export default [
  {
    path: '/admin/drafts',
    name: 'draft',
    component: () => import('./draftList'),
    meta: { title: '草稿管理' }
  }
];
