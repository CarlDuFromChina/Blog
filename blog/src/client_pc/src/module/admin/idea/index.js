export default [
  {
    path: '/admin/idea',
    name: 'idea',
    component: () => import('./ideaList'),
    meta: { title: '想法' }
  }
];
