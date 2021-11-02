import React from "react";
import "../custom.css";

const SQL07 = () => {
  return (
    <React.Fragment>
      <h1>SQL-07</h1>
      <h5>
        Udskriv alle kunder fra databasen, som har afgivet en ordre, samt lad
        det fremgå, hvor mange ordrer hver kunde har liggende i systemet.
      </h5>

      <h5>
        Udskriv igen alle kunder fra databasen, hvor alle deres ordrer udskrives
        sammen med kunden.
      </h5>

      <h5>Udskriv alle de kunder, der har købt “Blå Perler”.</h5>

      <h5>Opret en dropdownliste, med alle bolchenavne, samt en submitknap.</h5>

      <h5>
        Når et bolche vælges og der klikkes på knappen, vises alle de kunder,
        som har købt bolchet, samt selve ordren eller hvis der er flere, alle
        ordrerne. Listen sorteres efter kunde og derefter dato.
      </h5>
    </React.Fragment>
  );
};

export default SQL07;
