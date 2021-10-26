import React from "react";

const SQL02 = () => {
  return (
    <React.Fragment>
      <h1>SQL-02</h1>
      <h4>Udskriv alle informationer om alle bolcher.</h4>
      <h5>SELECT Id,ColorId,SournessId,StrengthId,FlavourId,Weight,ProductionCost,Name FROM dbo.Candy</h5>

      <h4>Find og udskriv navnene på alle de røde bolcher.</h4>
      <h4>
        Find og udskriv navnene på alle de røde og de blå bolcher, i samme SQL
        udtræk.
      </h4>
      <h4>
        Find og udskriv navnene på alle bolcher, der ikke er røde, sorteret
        alfabetisk.
      </h4>
      <h4>Find og udskriv navnene på alle bolcher som starter med et “B”.</h4>
      <h4>
        Find og udskriv navene på alle bolcher, hvor der i navnet findes mindst
        ét “e”.
      </h4>
      <h4>
        Find og udskriv navn og vægt på alle bolcher der vejer mindre end 10
        gram, sorter dem efter vægt, stigende.
      </h4>
      <h4>
        Find og udskriv navnene på alle bolcher, der vejer mellem 10 og 12 gram
        (begge tal inklusive), sorteret alfabetisk og derefter vægt.
      </h4>
      <h4>Vælg de tre største (tungeste) bolcher.</h4>
      <h4>
        Udskriv alle informationer om et tilfældigt bolche, udvalgt af systemet.
      </h4>
    </React.Fragment>
  );
};

export default SQL02;
