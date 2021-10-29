import React, { useEffect, useState } from "react";
import "../custom.css";
import { Table } from "reactstrap";

const SQL04 = () => {
  const [candies, setCandies] = useState([]);
  let nettoPrice = 0;
  let salesPrice = 0;
  let weightPrice = 0;

  const fetchCandies = async () => {
    let url = "http://172.16.6.27:5000/api/Candies/";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandies(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  };

  useEffect(() => {
    fetchCandies();
  }, [])

  return (
    <React.Fragment>
      <h1>SQL-04</h1>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Vægt</th>
            <th>Råvarepris (øre)</th>
            <th>Nettopris</th>
            <th>Salgspris</th>
            <th>Salgspris per 100g</th>
          </tr>
        </thead>
        <tbody>
          {candies.map((candy) => (
            <tr key={candy.id}>
              <th scope="row">{candy.name}</th>
              <td>{candy.weight}</td>
              <td>{candy.productionCost}</td>
              {nettoPrice = candy.productionCost + candy.productionCost*2.5}
              {salesPrice = nettoPrice + nettoPrice*0.25}
              {weightPrice = (salesPrice/candy.weight) * 100}
              <td>{nettoPrice.toFixed(1)}</td>
              <td>{salesPrice.toFixed(1)}</td>
              <td>{weightPrice.toFixed(1)}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </React.Fragment>
  );
};

export default SQL04;
