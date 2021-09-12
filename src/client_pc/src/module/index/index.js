import home from './home';
import friends from './friends';
import readingNote from './readingNote';

export default [
  {
    path: '/index',
    name: 'index',
    component: () => import('./index.vue'),
    redirect: '/index/home',
    children: [].concat(home, friends, readingNote)
  }
];
