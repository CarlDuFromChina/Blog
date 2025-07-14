import Vue from 'vue';
import spIcon from './spIcon';
import spSection from './spSection';
import spMenu from './spMenu';
import spMenuItem from './spMenuItem';
import spBlogCard from './spBlogCard';
import spCard from './spCard';
import blogMenu from './blogMenu';

const components = [
  { name: spIcon.name, component: spIcon },
  { name: spSection.name, component: spSection },
  { name: spMenu.name, component: spMenu },
  { name: spMenuItem.name, component: spMenuItem },
  { name: spBlogCard.name, component: spBlogCard },
  { name: spCard.name, component: spCard },
  { name: blogMenu.name, component: blogMenu }
];

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

Vue.use(install);
