import React from 'react';
import './App.css';
import { makeStyles } from "@material-ui/core/styles"
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom"
import { Drawer, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core"
import Produto from './pages/produto';
import ProdutoList from './pages/produto-list';
import FormatListBulletedIcon from '@material-ui/icons/FormatListBulleted';
import AddCircleOutlineIcon from '@material-ui/icons/AddCircleOutline';
import ProdutoEdit from './pages/produto-edit';

const useStyles = makeStyles((theme) => ({
  drawerPaper: { width: 'inherit' },
  link: {
    textDecoration: 'none',
    color: theme.palette.text.primary
  }
}))

function App() {
  const classes = useStyles()
  return (
    <Router>
      <div style={{ display: 'flex' }}>
        <Drawer
          style={{ width: '220px' }}
          variant="persistent"
          anchor="left"
          open={true}
          classes={{ paper: classes.drawerPaper }}
        >
          <List>
            <Link to="/produto-list" className= { classes.link }>
              <ListItem>
                <ListItemIcon button>
                  <FormatListBulletedIcon/>
                </ListItemIcon>
                <ListItemText primary={ "Consulta de Produtos" }></ListItemText>
              </ListItem>
            </Link>
            <Link to="/produto" className= { classes.link }>
              <ListItem>
                <ListItemIcon button>
                  <AddCircleOutlineIcon/>
                </ListItemIcon>
                <ListItemText primary={ "Cadastro de Produto" }></ListItemText>
              </ListItem>
            </Link>
          </List>
        </Drawer>
        <Switch>
          <Route path="/produto-list" component={ ProdutoList } />
          <Route path="/produto" component={ Produto } />
          <Route path="/produto-edit/:id" component={ ProdutoEdit } />
        </Switch>
      </div>
    </Router>
  );
}

export default App;