import home from './home';
import friends from './friends';
import readingNote from './readingNote';
import messageRemind from './messageRemind';

export default [
  {
    path: '/index',
    name: 'index',
    component: () => import('./index.vue'),
    redirect: '/index/home',
    children: [].concat(home, friends, readingNote, messageRemind)
  }
];
