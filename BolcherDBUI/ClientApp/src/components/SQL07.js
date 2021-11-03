import React, { useEffect, useState } from "react";
import { Input } from "reactstrap";
import Table from "reactstrap/lib/Table";
import "../custom.css";

const SQL07 = () => {
  const [customersWithSalesOrders, setCustomersWithSalesOrders] = useState([]);
  const [customersWithSpecificCandy, setCustomersWithSpecificCandy] = useState(
    []
  );
  const [customersWithBlueCandy, setCustomersWithBlueCandy] = useState([]);
  const [candies, setCandies] = useState([]);
  const [selectedCandyId, setSelectedCandyId] = useState(0);

  const fetchCustomersWithSalesOrders = async () => {
    let url = "http://172.16.6.27:5000/api/Customers?has_orders=true";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setCustomersWithSalesOrders(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchCustomersWithBluePerlCandy = async () => {
    let url = "http://172.16.6.27:5000/api/Customers/candy/8";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setCustomersWithBlueCandy(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchCustomersWithSpecificCandy = async (candyId) => {
    let url = "http://172.16.6.27:5000/api/Customers/candy/"+candyId;
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setCustomersWithSpecificCandy(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchCandies = async () => {
    let url = "http://172.16.6.27:5000/api/Candies";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandies(loadedCandies);
      if (loadedCandies.length === 0) {
      }
    } catch (error) {
      console.log(error);
    }
  };

  const onChangeHandler = event => {
    const { value } = event.target;
    const candyId = parseInt(value);
    fetchCustomersWithSpecificCandy(candyId);
    setSelectedCandyId(candyId);
  }

  useEffect(() => {
    fetchCustomersWithSalesOrders();
    fetchCustomersWithBluePerlCandy();
    fetchCandies();
  }, []);

  return (
    <React.Fragment>
      <h1>SQL-07</h1>
      <h5>
        Udskriv alle kunder fra databasen, som har afgivet en ordre, samt lad
        det fremgå, hvor mange ordrer hver kunde har liggende i systemet.
      </h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Antal ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWithSalesOrders.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>{customer.salesOrders.length}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>
        Udskriv igen alle kunder fra databasen, hvor alle deres ordrer udskrives
        sammen med kunden.
      </h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWithSalesOrders.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>
                <Table>
                  <thead>
                    <tr>
                      <th>Id</th>
                      <th>Antal forskellige bolcher</th>
                      <th>Dato</th>
                    </tr>
                  </thead>
                  <tbody>
                    {customer.salesOrders.map((salesOrder) => (
                      <tr>
                        <td>{salesOrder.id}</td>
                        <td>{salesOrder.orderLines.length}</td>
                        <td>{salesOrder.orderDate}</td>
                      </tr>
                    ))}
                  </tbody>
                </Table>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>Udskriv alle de kunder, der har købt “Blå Perler”.</h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Antal ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWithBlueCandy.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>{customer.salesOrders.length}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>Opret en dropdownliste, med alle bolchenavne, samt en submitknap.</h5>
      <h5>
        Når et bolche vælges og der klikkes på knappen, vises alle de kunder,
        som har købt bolchet, samt selve ordren eller hvis der er flere, alle
        ordrerne. Listen sorteres efter kunde og derefter dato.
      </h5>
      <br/>
      <Input
          type="select"
          name="color"
          value={selectedCandyId}
          onChange={onChangeHandler}
          >
          <option onClick={onChangeHandler} key={0} value={0}>
            Vælg bolche
          </option>
          {candies.map((candy) => (
            <option onClick={onChangeHandler} key={candy.id} value={candy.id}>
              {candy.name}
            </option>
          ))}
        </Input>
        <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWithSpecificCandy.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>
                <Table>
                  <thead>
                    <tr>
                      <th>Id</th>
                      <th>Antal forskellige bolcher</th>
                      <th>Dato</th>
                    </tr>
                  </thead>
                  <tbody>
                    {customer.salesOrders.map((salesOrder) => (
                      <tr>
                        <td>{salesOrder.id}</td>
                        <td>{salesOrder.orderLines.length}</td>
                        <td>{salesOrder.orderDate}</td>
                      </tr>
                    ))}
                  </tbody>
                </Table>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </React.Fragment>
  );
};

export default SQL07;
