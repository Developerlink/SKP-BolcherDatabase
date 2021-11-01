import React, { useEffect, useState } from "react";
import { Button, Input, Row } from "reactstrap";
import "../custom.css";
import CandyTable from "./CandyTable";

const SQL03 = () => {
  const [candies, setCandies] = useState([]);
  const [colors, setColors] = useState([]);
  const [selectedColorID, setSelectedColorID] = useState(0);
  const [showingError, setShowingError] = useState(false);
  const [showingNoResultsMessage, setShowingNoResultsMessage] = useState();

  const fetchCandiesBySearch = async (queryType, search) => {
    let url = "";
    if (queryType === "starts") {
      console.log("starts");
      if (selectedColorID === 0) {
        url =
          "http://172.16.6.27:5000/api/Candies/search?starts_with=" + search;
        } else {
          url =
          "http://172.16.6.27:5000/api/Candies/search?starts_with=" +
          search +
          "&colorId=" +
          selectedColorID;
      }
    } else if (queryType === "contains") {
      if (selectedColorID === 0) {
        url = "http://172.16.6.27:5000/api/Candies/search?contains=" + search;
      } else {
        url =
        "http://172.16.6.27:5000/api/Candies/search?contains=" +
        search +
        "&colorId=" +
        selectedColorID;
      }      
    }
    try {
      setShowingNoResultsMessage(false);
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandies(loadedCandies);
      console.log(loadedCandies);
      if (loadedCandies.length === 0){
        setShowingNoResultsMessage(true);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const fetchCandies = async () => {
    let url = "";
    if (selectedColorID === 0) {
      url = "http://172.16.6.27:5000/api/Candies/";
    } else {
      url =
        "http://172.16.6.27:5000/api/Candies/color?color_id=" + selectedColorID;
    }
    try {
      setShowingNoResultsMessage(false);
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandies(loadedCandies);
      if (loadedCandies.length === 0){
        setShowingNoResultsMessage(true);
      }
    } catch (error) {
      console.log(error);
    }
  };

  const fetchCandiesWithNamesContaining = async (search) => {
    fetchCandiesBySearch("contains", search);
  };

  const fetchCandiesWithNamesStartingWith = async (search) => {
    fetchCandiesBySearch("starts", search);
  };

  const showAllHandler = async () => {
    setShowingError(false);
    fetchCandies();
  };

  const onKeyPressHandler = async (event) => {
    const { value: search, name } = event.target;
    if (event.charCode === 13) {
      setCandies([]);
      if (search) {
        setShowingError(false);
        if (name === "contains") {
          fetchCandiesWithNamesContaining(search);
        } else if (name === "starts") {
          fetchCandiesWithNamesStartingWith(search);
        }
      } else {
        setShowingError(true);
        setShowingNoResultsMessage(false);
      }
    }
  };

  const fetchColors = async () => {
    let url = "http://172.16.6.27:5000/api/Colors";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedColors = await response.json();
      setColors(loadedColors);
    } catch (error) {
      console.log(error.message);
    }
  };

  useEffect(() => {
    fetchColors();
  }, []);

  const onChangeHandler = async (event) => {
    const { value } = event.target;
    console.log(value);
    setSelectedColorID(parseInt(value));
  };

  return (
    <React.Fragment>
      <h1>SQL-03</h1>
      <Row>
        <Input
          className="search"
          name="contains"
          type="text"
          placeholder="søg bolcher som indeholder"
          onKeyPress={onKeyPressHandler}
          />
        <Input
          className="search"
          name="starts"
          type="text"
          placeholder="søg bolcher som starter med"
          onKeyPress={onKeyPressHandler}
        />
        <Input
          className="search"
          type="select"
          name="color"
          value={selectedColorID}
          onChange={onChangeHandler}
          >
          <option onClick={onChangeHandler} key={0} value={0}>
            Vælg farve
          </option>
          {colors.map((color) => (
            <option onClick={onChangeHandler} key={color.id} value={color.id}>
              {color.name}
            </option>
          ))}
        </Input>
        <Button className="search" onClick={showAllHandler} color="primary">
          Vis alle
        </Button>
      </Row>
      {candies && <CandyTable candies={candies} />}
      {showingError && <h5>Vælg søgekriterier eller klik på <em>Vis Alle</em></h5>}
      {showingNoResultsMessage && <h5>Der er ingen bolcher der matcher din søgning</h5>}
    </React.Fragment>
  );
};

export default SQL03;
