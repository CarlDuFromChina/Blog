export default [
  {
    path: '/admin/notification',
    name: 'notification',
    component: () => import('./notification'),
    meta: { title: '消息通知' }
  }
];
