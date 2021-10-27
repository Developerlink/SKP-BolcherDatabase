import React from "react";
import "../custom.css";
import CandyTable from "./CandyTable";

const SQL02 = () => {
  return (
    <React.Fragment>
      <h1>SQL-02</h1>
      <h5>Udskriv alle informationer om alle bolcher.</h5>
      <h6>
        SELECT Id, ColorId, SournessId, StrengthId, FlavourId, Weight, ProductionCost, Name FROM Candy
      </h6>
      <CandyTable candies={[]}/>

      <h5>Find og udskriv navnene på alle de røde bolcher.</h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE (ColorId = 4)
      </h6>

      <h5>
        Find og udskriv navnene på alle de røde og de blå bolcher, i samme SQL
        udtræk.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE ColorId = 4 OR ColorId = 8
      </h6>

      <h5>
        Find og udskriv navnene på alle bolcher, der ikke er røde, sorteret
        alfabetisk.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE ColorId {"<>"} 4
      </h6>

      <h5>Find og udskriv navnene på alle bolcher som starter med et “B”.</h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE LOWER(Name) LIKE 'b%'
      </h6>

      <h5>
        Find og udskriv navene på alle bolcher, hvor der i navnet findes mindst
        ét “e”.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE LOWER(Name) LIKE '%e%'
      </h6>

      <h5>
        Find og udskriv navn og vægt på alle bolcher der vejer mindre end 10
        gram, sorter dem efter vægt, stigende.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE Weight {"<"} 10
      </h6>

      <h5>
        Find og udskriv navnene på alle bolcher, der vejer mellem 10 og 12 gram
        (begge tal inklusive), sorteret alfabetisk og derefter vægt.
      </h5>
      <h6>
        SELECT Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy WHERE (Weight {">="} 10) AND (Weight{" "}
        {"<="} 12) ORDER BY Name, Weight{" "}
      </h6>

      <h5>Vælg de tre største (tungeste) bolcher.</h5>
      <h6>
        SELECT TOP 3 Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy ORDER BY Weight DESC
      </h6>

      <h5>
        Udskriv alle informationer om et tilfældigt bolche, udvalgt af systemet.
      </h5>
      <h6>
        SELECT TOP(1) Id, ColorId, FlavourId, Name, ProductionCost, SournessId,
        StrengthId, Weight FROM Candy ORDER BY NEWID()
      </h6>
    </React.Fragment>
  );
};

export default SQL02;
