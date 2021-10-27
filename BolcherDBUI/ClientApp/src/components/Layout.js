import React, { Component } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import { Route } from 'react-router';
import { Home } from './Home';
import { FetchData } from './FetchData';
import { Counter } from './Counter';
import SQL02 from "./SQL02";
import SQL03 from "./SQL03";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <Container>
          <Route exact path="/" component={SQL02} />
          <Route path="/sql03" component={SQL03} />
          <Route path="/sql04" component={SQL02} />
          <Route path="/sql05" component={SQL02} />
          <Route path="/sql06" component={SQL02} />
          <Route path="/sql07" component={SQL02} />
          <Route path="/sql08" component={SQL02} />
        </Container>
      </div>
    );
  }
}
