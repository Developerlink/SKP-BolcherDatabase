import React, { Component } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";
import { Route } from "react-router";
import SQL02 from "./SQL02";
import SQL03 from "./SQL03";
import SQL04 from "./SQL04";
import SQL05 from "./SQL05";
import SQL06 from "./SQL06";
import SQL07 from "./SQL07";
import SQL08 from "./SQL08";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <Container>
          <Route exact path="/" component={SQL02} />
          <Route path="/sql03" component={SQL03} />
          <Route path="/sql04" component={SQL04} />
          <Route path="/sql05" component={SQL05} />
          <Route path="/sql06" component={SQL06} />
          <Route path="/sql07" component={SQL07} />
          <Route path="/sql08" component={SQL08} />
        </Container>
      </div>
    );
  }
}
