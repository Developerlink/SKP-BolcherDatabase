import React, { useEffect, useState } from "react";
import { Col, Input, FormGroup, Label, Row, Button, Table } from "reactstrap";
import "../custom.css";

const SQL05 = () => {
  const [candies, setCandies] = useState([]);
  const [selectedCandyId, setSelectedCandy] = useState(0);
  const [cartedCandies, setCartedCandies] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [selectedCustomerId, setSelectedCustomer] = useState(0);
  const [salesOrders, setSalesOrders] = useState([]);

  const fetchCandies = async () => {
    let url = "http://172.16.6.27:5000/api/Candies/";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      let candiesWithAmount = [];
      loadedCandies.forEach((candy) => {
        candiesWithAmount.push({
          id: candy.id,
          name: candy.name,
          amount: 1,
        });
      });
      setCandies(candiesWithAmount);
      if (candiesWithAmount.length === 0) {
      }
    } catch (error) {
      console.log(error);
    }
  };

  const fetchCustomers = async () => {
    let url = "http://172.16.6.27:5000/api/Customers/";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCustomers = await response.json();
      setCustomers(loadedCustomers);
      if (loadedCustomers.length === 0) {
      }
    } catch (error) {
      console.log(error);
    }
  };

  const fetchSalesOrders = async () => {
    let url = "http://172.16.6.27:5000/api/salesorders";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedSalesOrders = await response.json();
      setSalesOrders(loadedSalesOrders);
    } catch (error) {
      console.log(error.message);
    }
  };

  const onSelectCustomerHandler = (event) => {
    const { value: customerId } = event.target;
    setSelectedCustomer(parseInt(customerId));
    setCartedCandies([]);
  };

  const onSelectCandyHandler = (event) => {
    const { value: candyId } = event.target;
    setSelectedCandy(parseInt(candyId));
    if (
      !cartedCandies.find((x) => x.id === parseInt(candyId)) &&
      parseInt(candyId) !== 0
    ) {
      let candy = candies.find((x) => x.id === parseInt(candyId));
      console.log(candy);
      setCartedCandies((prevValues) => [...prevValues, candy]);
    }
  };

  const onAmountChangeHandler = (event) => {
    const { value, id } = event.target;
    let items = [...cartedCandies];
    let index = items.findIndex((x) => x.id === parseInt(id));
    let item = {
      ...items[index],
      amount: parseInt(value),
    };
    items[index] = item;
    setCartedCandies(items);
  };

  const pushSalesOrder = async (salesOrder) => {
    let url = "http://172.16.6.27:5000/api/salesorders";
    try {
      const response = await fetch(url, {
        method: "POST",
        body: JSON.stringify(salesOrder),
        headers: {
          Accept: "application/json",
          "Content-type": "application/json",
        },
      });
      const data = await response.json();
      console.log(data);
    } catch (error) {
      console.log(error.message);
    }
  };

  const addOrderHandler = () => {
    let customerId;

    if (selectedCustomerId === 0) {
      customerId = customers[0].id;
      setSelectedCustomer(customerId);
    } else {
      customerId = selectedCustomerId;
    }

    let newDate = new Date();
    let newOrderLines = [];
    cartedCandies.forEach((candy) => {
      newOrderLines.push({
        candyId: candy.id,
        salesOrderId: 0,
        amount: candy.amount,
      });
    });

    let salesOrder = {
      customerId: customerId,
      orderDate: newDate,
      orderLines: newOrderLines,
    };

    console.log(salesOrder);

    pushSalesOrder(salesOrder);
    setCartedCandies([]);
    fetchSalesOrders();
  };

  useEffect(() => {
    fetchCandies();
    fetchCustomers();
    fetchSalesOrders();
  }, []);

  return (
    <React.Fragment>
      <h1>SQL-05</h1>
      <Row>
        <Col>
          <h3>Indtast ordre</h3>
          <FormGroup>
            <Label>Vælg kunde</Label>
            <Input
              type="select"
              name="customerSelection"
              value={selectedCustomerId}
              onChange={onSelectCustomerHandler}
            >
              {customers.map((customer) => (
                <option
                  onClick={onSelectCustomerHandler}
                  key={customer.id}
                  value={customer.id}
                >
                  {customer.firstName}
                </option>
              ))}
            </Input>
          </FormGroup>
          <FormGroup>
            <Label>Vælg bolche</Label>
            <Input
              type="select"
              name="candySelection"
              value={selectedCandyId}
              onChange={onSelectCandyHandler}
            >
              <option value={0}>-</option>
              {candies.map((candy) => (
                <option
                  onClick={onSelectCandyHandler}
                  key={candy.id}
                  value={candy.id}
                >
                  {candy.name}
                </option>
              ))}
            </Input>
          </FormGroup>
          <FormGroup>
            <Label>Købsliste</Label>
            <Table>
              <thead>
                <tr>
                  <th>Navn</th>
                  <th>Antal</th>
                </tr>
              </thead>
              <tbody>
                {cartedCandies.map((candy) => (
                  <tr key={candy.id}>
                    <th scope="row">{candy.name}</th>
                    <td>
                      <Input
                        key={candy.id}
                        id={candy.id}
                        type="number"
                        value={candy.amount}
                        step="1"
                        min={0}
                        max={1000}
                        onChange={onAmountChangeHandler}
                      />
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </FormGroup>
          <div className="button-right">
            <FormGroup>
              <Button
                className="save-button"
                color="primary"
                onClick={addOrderHandler}
              >
                Tilføj
              </Button>
            </FormGroup>
          </div>
        </Col>
        <Col>
          <h3>Ordrer</h3>
          <Table>
              <thead>
                <tr>
                  <th>Id</th>
                  <th>Kunde</th>
                  <th>Antal typer a bolcher</th>
                </tr>
              </thead>
              <tbody>
                {salesOrders.map((salesOrder) => (
                  <tr key={salesOrder.id}>
                    <th scope="row">{salesOrder.id}</th>
                    <td>{salesOrder.customer.firstName}</td>
                    <td>{salesOrder.orderLines.length}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
        </Col>
      </Row>
    </React.Fragment>
  );
};

export default SQL05;
