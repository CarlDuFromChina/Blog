import spHeader from './spHeader';
import spView from './spView';
import spContent from './spContent';
import spIcon from './spIcon';
import spFooter from './spFooter';

const components = [
  { name: spHeader.name, component: spHeader },
  { name: spView.name, component: spView },
  { name: spContent.name, component: spContent },
  { name: spFooter.name, component: spFooter },
  { name: spIcon.name, component: spIcon }
];

const install = _Vue => {
  components.forEach(item => {
    _Vue.component(item.name, item.component);
  });
};

export default install;
