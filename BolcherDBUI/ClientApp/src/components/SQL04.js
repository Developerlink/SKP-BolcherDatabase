import React, { useEffect, useState } from "react";
import "../custom.css";
import { Table } from "reactstrap";

const SQL04 = () => {
  const [candies, setCandies] = useState([]);

  const fetchCandies = async () => {
    let url = "http://172.16.6.27:5000/api/Candies/";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      let calculatedCandies = []
      loadedCandies.forEach(candy => {
        let nettoPrice = candy.productionCost + candy.productionCost*2.5;
        let salesPrice = nettoPrice + nettoPrice*0.25;
        let weightPrice = (salesPrice/candy.weight);
        calculatedCandies.push({
          ...candy,
          nettoPrice: nettoPrice,
          salesPrice: salesPrice,
          weightPrice: weightPrice
        })        
      });
      setCandies(calculatedCandies);
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
            <th>Råvarepris (øre/stk)</th>
            <th>Nettopris (øre/stk)</th>
            <th>Salgspris (øre/stk)</th>
            <th>Salgspris (kr/100g)</th>
          </tr>
        </thead>
        <tbody>
          {candies.map((candy) => (
            <tr key={candy.id}>
              <th scope="row">{candy.name}</th>
              <td>{candy.weight}</td>
              <td>{candy.productionCost}</td>
              <td>{candy.nettoPrice.toFixed(1)}</td>
              <td>{candy.salesPrice.toFixed(1)}</td>
              <td>{candy.weightPrice.toFixed(1)}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </React.Fragment>
  );
};

export default SQL04;
