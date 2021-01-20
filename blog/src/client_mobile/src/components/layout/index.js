import spHeader from './spHeader';
import spView from './spView';
import spContent from './spContent';
import spFooter from './spFooter';

export default [
  { name: spHeader.name, component: spHeader },
  { name: spView.name, component: spView },
  { name: spContent.name, component: spContent },
  { name: spFooter.name, component: spFooter }
];
