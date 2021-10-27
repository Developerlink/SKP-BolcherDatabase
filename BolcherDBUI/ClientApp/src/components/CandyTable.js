import React from 'react';
import { Table } from 'reactstrap';

const CandyTable = (props) => {
    return (
      <Table>
        <thead>
          <tr>
            <th>Navn</th>
            <th>Farve</th>
            <th>Surhed</th>
            <th>Smag</th>
            <th>Vægt</th>
            <th>Råvarepris (øre)</th>
          </tr>
        </thead>
        <tbody>
            {props.candies.map((candy) => (
                <tr>
                <th scope="row"></th>
                <td>Mark</td>
                <td>Otto</td>
                <td>@mdo</td>
              </tr>
            ))}          
        </tbody>
      </Table>
    );
  }
  
  export default CandyTable;