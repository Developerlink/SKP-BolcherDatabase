import React, { useEffect, useState } from "react";
import Table from "reactstrap/lib/Table";
import "../custom.css";

const SQL08 = () => {
  const [customer, setCustomer] = useState({
    firstName: "",
    salesOrders: [],
  });
  const [
    customersWhoBoughtMoreThan100Grams,
    setCustomersWhoBoughtMoreThan100Grams,
  ] = useState([]);
  const [customersWhoBoughtStrongCandies, setCustomersWhoBoughtStrongCandies] =
    useState([]);
  const [allCustomers, setAllCustomers] = useState([]);
  const [customersWhoBoughtForMoreThan5, setCustomersWhoBoughtForMoreThan5] =
    useState([]);
  const [customersWithBlueSharkCandy, setCustomersWithBlueSharkCandy] =
    useState([]);

  const fetchCustomer = async () => {
    let url = "http://172.16.6.27:5000/api/Customers/2005";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomer = await re.json();
      setCustomer(loadedCustomer);
    } catch (error) {}
  };

  const fetchCustomersWhoBoughtMoreThan100Grams = async () => {
    let url =
      "http://172.16.6.27:5000/api/Customers?has_orders=true&above100=true";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setCustomersWhoBoughtMoreThan100Grams(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchCustomersWhoBoughtStrongCandies = async () => {
    let url = "http://172.16.6.27:5000/api/Customers/candy/strength/3";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setCustomersWhoBoughtStrongCandies(loadedCustomers);
    } catch (error) {}
  };

  const fetchAllCustomers = async () => {
    let url = "http://172.16.6.27:5000/api/Customers";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setAllCustomers(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchCustomersWhoBoughtForMoreThan5 = async () => {
    let url =
      "http://172.16.6.27:5000/api/Customers?has_orders=true&above_amount=5";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setCustomersWhoBoughtForMoreThan5(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  const fetchCustomersWithBlueSharkCandy = async () => {
    let url = "http://172.16.6.27:5000/api/Customers/candy/6";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCustomers = await re.json();
      setCustomersWithBlueSharkCandy(loadedCustomers);
    } catch (error) {
      console.log(error.message);
    }
  };

  useEffect(() => {
    fetchCustomer();
    fetchCustomersWhoBoughtMoreThan100Grams();
    fetchCustomersWhoBoughtStrongCandies();
    fetchAllCustomers();
    fetchCustomersWhoBoughtForMoreThan5();
    fetchCustomersWithBlueSharkCandy();
  }, []);

  return (
    <React.Fragment>
      <h1>SQL-08</h1>
      <h5>Hent alle ordrer fra kunde nummer 4 ud.</h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Ordrer</th>
          </tr>
        </thead>
        <tbody>
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
        </tbody>
      </Table>

      <h5>Lav en liste over alle kunder der har købt mere end 100g bolcher.</h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Antal ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWhoBoughtMoreThan100Grams.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>{customer.salesOrders.length}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>
        Lav en liste over alle kunder der bor i samme by som dig og har købt
        stærke bolcher.
      </h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Antal ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWhoBoughtStrongCandies.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>{customer.salesOrders.length}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>Lav en liste over alle kunder og gruppér dem efter antal ordre.</h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Antal ordrer</th>
          </tr>
        </thead>
        <tbody>
          {allCustomers.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>{customer.salesOrders.length}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>Lav en liste over alle dem der har købt for over 5kr.</h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Antal ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWhoBoughtForMoreThan5.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>{customer.salesOrders.length}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <h5>
        Lav en liste over alle dem der har købt en eller flere blå haj bolcher
      </h5>
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Antal ordrer</th>
          </tr>
        </thead>
        <tbody>
          {customersWithBlueSharkCandy.map((customer) => (
            <tr>
              <td>{customer.firstName}</td>
              <td>{customer.salesOrders.length}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </React.Fragment>
  );
};

export default SQL08;
