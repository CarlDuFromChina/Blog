export default [
  {
    path: '/admin/job',
    name: 'job',
    component: () => import('./job'),
    meta: { title: '作业管理' }
  }
];
