import home from './home';
import aboutme from './aboutme';
import friends from './friends';

export default [
  {
    path: '/index',
    name: 'index',
    component: () => import('./index.vue'),
    redirect: '/index/home',
    children: [].concat(home, aboutme, friends)
  },
  {
    path: '/',
    redirect: 'index'
  }
];
