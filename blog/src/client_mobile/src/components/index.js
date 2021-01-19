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

export default components;
