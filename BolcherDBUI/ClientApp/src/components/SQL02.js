import React, { useCallback, useEffect, useState } from "react";
import "../custom.css";
import CandyTable from "./CandyTable";

const SQL02 = () => {
  const [candies, setCandies] = useState([]);
  const [oneColorCandies, setOneColorCandies] = useState([]);
  const [twoColorCandies, setTwoColorCandies] = useState([]);
  const [notColorCandies, setNotColorCandies] = useState([]);
  const [candiesStartingWithB, setCandiesStartingWithB] = useState([]);
  const [candiesContainingE, setCandiesContainingE] = useState([]);
  const [candiesWeighingLess, setCandiesWeighingLess] = useState([]);
  const [candiesWeighingBetween, setCandiesWeighingBetween] = useState([]);
  const [heaviestCandies, setHeaviestCandies] = useState([]);
  const [randomCandy, setRandomCandy] = useState([]);

  const fetchCandiesHandler = useCallback(async () => {
    let url = "http://172.16.6.27:5000/api/Candies/";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandies(loadedCandies);
    } catch (error) {
      console.log(error);
    }
  }, []);

  const fetchOneColorCandiesHandler = useCallback(async () => {
    let url = "http://172.16.6.27:5000/api/Candies/color?color_id=4";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setOneColorCandies(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchTwoColorCandiesHandler = useCallback(async () => {
    let url =
      "http://172.16.6.27:5000/api/Candies/color?color_id=4&color2_id=8";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setTwoColorCandies(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchNotColorCandiesHandler = useCallback(async () => {
    let url = "http://172.16.6.27:5000/api/Candies/color?not_color_id=4";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setNotColorCandies(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchCandiesStartingWithHandler = useCallback(async () => {
    let url = "http://172.16.6.27:5000/api/Candies/search?starts_with=b";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandiesStartingWithB(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchCandiesContainingHandler = useCallback(async () => {
    let url = "http://172.16.6.27:5000/api/Candies/search?contains=e";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandiesContainingE(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchCandiesWeighingLess = useCallback(async () => {
    let url = "http://172.16.6.27:5000/api/Candies/weight?upper_limit=10";
    console.log("test");
    try {
    console.log("test2");

      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      console.log(loadedCandies);
      setCandiesWeighingLess(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchCandiesWeighingBetweenHandler = useCallback(async () => {
    let url =
      "http://172.16.6.27:5000/api/Candies/weight?upper_limit=12&lower_limit=10";
    try {
      const response = await fetch(url, { method: "GET" });
      const loadedCandies = await response.json();
      setCandiesWeighingBetween(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchHeaviestCandiesHandler = useCallback(async () => {
    let url =
      "http://172.16.6.27:5000/api/Candies/weight/heaviest?take_amount=3";
    try {
      const re = await fetch(url, { method: "GET" });
      const loadedCandies = await re.json();
      setHeaviestCandies(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  const fetchRandomCandyHandler = useCallback(async () => {
    let url = "http://172.16.6.27:5000/api/Candies/random";
    try {
      const loadedCandies = [];
      const re = await fetch(url, { method: "GET" });
      const loadedCandy = await re.json();
      loadedCandies.push(loadedCandy);
      setRandomCandy(loadedCandies);
    } catch (error) {
      console.log(error.message);
    }
  }, []);

  useEffect(() => {
    fetchCandiesHandler();
    fetchOneColorCandiesHandler();
    fetchTwoColorCandiesHandler();
    fetchNotColorCandiesHandler();
    fetchCandiesStartingWithHandler();
    fetchCandiesContainingHandler();
    fetchCandiesWeighingLess();
    fetchCandiesWeighingBetweenHandler();
    fetchHeaviestCandiesHandler();
    fetchRandomCandyHandler();

    return () => {};
  }, [
    fetchCandiesHandler,
    fetchOneColorCandiesHandler,
    fetchTwoColorCandiesHandler,
    fetchNotColorCandiesHandler,
    fetchCandiesStartingWithHandler,
    fetchCandiesContainingHandler,
    fetchCandiesWeighingLess,
    fetchCandiesWeighingBetweenHandler,
    fetchHeaviestCandiesHandler,
    fetchRandomCandyHandler,
  ]);

  return (
    <React.Fragment>
      <h1>SQL-02</h1>
      <h5>Udskriv alle informationer om alle bolcher.</h5>
      <h6>
        SELECT Id, ColorId, SournessId, StrengthId, FlavourId, Weight,
        ProductionCost, Name FROM Candy
      </h6>
      <CandyTable candies={candies} />

      <h5>Find og udskriv navnene på alle de røde bolcher.</h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE (ColorId = 4)
      </h6>
      <CandyTable candies={oneColorCandies} />

      <h5>
        Find og udskriv navnene på alle de røde og de blå bolcher, i samme SQL
        udtræk.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE ColorId = 4 OR ColorId = 8
      </h6>
      <CandyTable candies={twoColorCandies} />

      <h5>
        Find og udskriv navnene på alle bolcher, der ikke er røde, sorteret
        alfabetisk.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE ColorId {"<>"} 4
      </h6>
      <CandyTable candies={notColorCandies} />

      <h5>Find og udskriv navnene på alle bolcher som starter med et “B”.</h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE LOWER(Name) LIKE 'b%'
      </h6>
      <CandyTable candies={candiesStartingWithB} />

      <h5>
        Find og udskriv navene på alle bolcher, hvor der i navnet findes mindst
        ét “e”.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE LOWER(Name) LIKE '%e%'
      </h6>
      <CandyTable candies={candiesContainingE} />

      <h5>
        Find og udskriv navn og vægt på alle bolcher der vejer mindre end 10
        gram, sorter dem efter vægt, stigende.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE Weight {"<"} 10 ORDER BY Weight
      </h6>
      <CandyTable candies={candiesWeighingLess} />

      <h5>
        Find og udskriv navnene på alle bolcher, der vejer mellem 10 og 12 gram
        (begge tal inklusive), sorteret alfabetisk og derefter vægt.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE (Weight {">="} 10) AND (Weight{" "}
        {"<="} 12) ORDER BY Name, Weight{" "}
      </h6>
      <CandyTable candies={candiesWeighingBetween} />

      <h5>Vælg de tre største (tungeste) bolcher.</h5>
      <h6>
        SELECT TOP 3 Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy ORDER BY Weight DESC
      </h6>
      <CandyTable candies={heaviestCandies} />

      <h5>
        Udskriv alle informationer om et tilfældigt bolche, udvalgt af systemet.
      </h5>
      <h6>
        SELECT TOP(1) Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy ORDER BY NEWID()
      </h6>
      <CandyTable candies={randomCandy} />
    </React.Fragment>
  );
};

export default SQL02;
