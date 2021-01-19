import blog from './blog';
import idea from './idea';
import reading from './reading';
import friend from './friend';

export default [{
  path: '/',
  redirect: 'index'
}, {
  name: 'index',
  path: '/index',
  component: () => import('./index.vue'),
  children: [].concat(blog, idea, reading, friend),
  redirect: '/index/blogList'
}];
