import Vue from 'vue';

import heightLight from './highlight';

const directives = [
  heightLight
];

directives.forEach(e => {
  Vue.directive(e.name, e);
});
