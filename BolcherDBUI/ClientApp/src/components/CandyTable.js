import React from "react";
import { Table } from "reactstrap";

const CandyTable = (props) => {
  return (
    <Table>
      <thead>
        <tr>
          <th>Navn</th>
          <th>Farve</th>
          <th>Surhed</th>
          <th>Smag</th>
          <th>Styrke</th>
          <th>Vægt</th>
          <th>Råvarepris (øre)</th>
        </tr>
      </thead>
      <tbody>
        {props.candies.map((candy) => (
          <tr key={candy.id}>
            <th scope="row">{candy.name}</th>
            <td>{candy.color.name}</td>
            <td>{candy.sourness.name}</td>
            <td>{candy.flavour.name}</td>
            <td>{candy.strength.name}</td>
            <td>{candy.weight}</td>
            <td>{candy.productionCost}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};

export default CandyTable;
