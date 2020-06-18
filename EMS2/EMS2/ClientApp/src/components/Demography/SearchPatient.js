import React, { Component } from 'react';
import { Link,NavLink } from 'react-router-dom';

export class SearchPatient extends Component {
 
    constructor(props) {
        super(props);
        this.state = {
            HCN: "",
            FirstName: "",
            LastName:""
        };
        this.inputHandle = this.inputHandle.bind(this);
        this.searchCardNum = this.searchCardNum.bind(this);
        this.searchName = this.searchName.bind(this);
        this.displayItem = this.displayItem.bind(this);
    }
    inputHandle(event) {
        this.setState({
            [event.target.id] : event.target.value
        })
        console.log(this.state);
    }

    async searchCardNum() {

        const response = await fetch(`/api/patients/${this.state.HCN}`)
        const data = await response.json();

        this.displayItem(data);
    }
    async searchName() {
        const response = await fetch(`/api/patients/${this.state.FirstName} ${this.state.LastName}`)
        const data = await response.json();
    }

    displayItem(data) {
        console.log(data);
        
        const tBody = document.getElementById('result');
        tBody.innerHTML = '';

        let tr = tBody.insertRow();
        if (data.status >= 400) {
            let td = tr.insertCell(0);
            let item = document.createTextNode("no result found");
            td.appendChild(item);
            return 
        }
        Object.keys(data).forEach((key, i) => {
            let td = tr.insertCell(i);
            let item = document.createTextNode(data[key]);
            td.appendChild(item);
        })

        let updateBtn = document.createElement('a');
        updateBtn.href = `/demography/update/${this.state.HCN}`;
        updateBtn.text = "edit";
        
        let td = tr.insertCell(tr.cells.length);
        td.appendChild(updateBtn);
    }

    render() {
        return (
            <div>
                <h1>Search</h1>

                <h3>search by HCN</h3>
                <label for="HCN">Health Card Number</label><br />
                <input type="text" id="HCN" onChange={this.inputHandle} ></input><br />
                <br />
                <input type="button" value="submit" onClick={this.searchCardNum}></input><br />

                <h3>search by full name</h3>
                <label for="FirstName">First Name</label><br/>
                <input type="text" id="FirstName" onChange={this.inputHandle}></input><br />

                <label for="LastName">Last Name</label><br />
                <input type="text" id="LastName" onChange={this.inputHandle}></input><br/>
                <br />
                <input type="button" value="submit"></input>

                <table>
                    <tr>
                        <th>HCN</th>
                        <th>Last Name</th>
                        <th>First Name</th>
                        <th>M Initial</th>
                        <th>Date Birth</th>
                        <th>Sex</th>
                        <th>Head of House</th>
                        <th>Address Line1</th>
                        <th>Address Line2</th>
                        <th>City</th>
                        <th>Province</th>
                        <th>Postal Code</th>
                        <th>Phone Number</th>
                        <th></th>
                    </tr>
                    <tbody id="result"></tbody>
                </table>
            </div>
        );
    }
}