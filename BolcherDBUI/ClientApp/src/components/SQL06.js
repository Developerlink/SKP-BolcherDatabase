import React, { useEffect, useState } from "react";
import { Label, Table } from "reactstrap";
import "../custom.css";

const SQL06 = () => {
  const [customers, setCustomers] = useState([]);
  const [salesOrdersByOrderDate, setSalesOrdersByOrderDate] = useState([]);
  const [latestSalesOrder, setLatestSalesOrder] = useState({
    id: 0,
    customerId: 0,
    orderDate: "",
    customer: {
      id: 0,
      firstName: "",
      lastName: "",
    },
    orderLines: [],
  });

  const fetchCustomers = async () => {
    let url = "http://172.16.6.27:5000/api/Customers";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCustomers = await response.json();
      setCustomers(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchSalesOrdersByOrderDate = async () => {
    let url = "http://172.16.6.27:5000/api/salesorders?order_by_date=true";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedSalesOrders = await response.json();
      let customizedSalesOrders = [];
      loadedSalesOrders.forEach((salesOrder) => {
        var totalAmount = salesOrder.orderLines.reduce((a, b) => {
          return a + b["amount"];
        }, 0);

        customizedSalesOrders.push({
          id: salesOrder.id,
          customer: salesOrder.customer.firstName,
          totalAmount: totalAmount,
          orderDate: salesOrder.orderDate,
        });
      });

      setSalesOrdersByOrderDate(customizedSalesOrders);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchLatestSalesOrder = async () => {
    let url = "http://172.16.6.27:5000/api/salesorders/latest";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedSalesOrder = await re.json();
      setLatestSalesOrder(loadedSalesOrder);
    } catch (error) {
      console.log(error.message);
    }
  };

  useEffect(() => {
    fetchCustomers();
    fetchSalesOrdersByOrderDate();
    fetchLatestSalesOrder();
  }, []);

  return (
    <React.Fragment>
      <h1>SQL-06</h1>
      <h5>
        Udskriv alle kunderne fra databasen, sorteret efter deres unikke
        identifikation.
      </h5>
      <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Fornavn</th>
            <th>Efternavn</th>
          </tr>
        </thead>
        <tbody>
          {customers.map((customer) => (
            <tr>
              <th scope="row">{customer.id}</th>
              <td>{customer.firstName}</td>
              <td>{customer.lastName}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>Udskriv alle ordrer fra databasen, sorteret efter ordredato.</h5>
      <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Kunde</th>
            <th>Antal Bolcher</th>
            <th>Dato</th>
          </tr>
        </thead>
        <tbody>
          {salesOrdersByOrderDate.map((salesOrder) => (
            <tr>
              <td>{salesOrder.id}</td>
              <td>{salesOrder.customer}</td>
              <td>{salesOrder.totalAmount}</td>
              <td>{salesOrder.orderDate}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>
        Udskriv den kunde, der har den seneste ordre, samt udskriv selve ordren
      </h5>
      <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Fornavn</th>
            <th>Efternavn</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <th scope="row">{latestSalesOrder.customer.id}</th>
            <td>{latestSalesOrder.customer.firstName}</td>
            <td>{latestSalesOrder.customer.lastName}</td>
          </tr>
        </tbody>
      </Table>
      <Label>Dato: {latestSalesOrder.orderDate}</Label>
      <Table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Bolche</th>
            <th>Antal</th>
          </tr>
        </thead>
        <tbody>
          {latestSalesOrder.orderLines.map((orderLine) => (
            <tr>
              <td>{orderLine.candyId}</td>
              <td>{orderLine.candy.name}</td>
              <td>{orderLine.amount}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </React.Fragment>
  );
};

export default SQL06;
