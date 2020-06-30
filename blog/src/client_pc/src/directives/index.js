import heightLight from './highlight';

const Vue = require('vue');

const directives = [
  heightLight
];

directives.forEach(e => {
  Vue.directive(e.name, e);
});
