import post from './post';
import error from './error';
import reading from './reading';
import idea from './idea';

export default [{
  path: '/',
  redirect: 'index'
}, {
  name: 'index',
  path: '/index',
  component: () => import('./index.vue'),
  children: [].concat(post, reading, idea),
  redirect: '/index/postList'
}].concat(error);
