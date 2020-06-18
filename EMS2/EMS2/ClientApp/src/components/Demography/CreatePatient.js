import React, { Component } from 'react';

export class CreatePatient extends Component {
    constructor(props) {
        super(props);
        this.state = {
            HCN: "",
            LastName: "",
            FirstName: "",
            MInitial: "",
            DateBirth: "",
            Sex: "",
            HeadOfHouse: "",
            AddressLine1: "",
            AddressLine2: "",
            City: "",
            Province: "",
            PostalCode: "",
            PhoneNumber:""
        }
        this.inputHandle = this.inputHandle.bind(this);
        this.createPatient = this.createPatient.bind(this);
    }
    inputHandle(event) {
        this.setState({
            [event.target.id]: event.target.value
        })
    }
    async createPatient() {
        const response = await fetch(`/api/patients/`, {
            method:'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type':'application/json'
            },
            body: JSON.stringify(this.state)
        })
        const data = await response.json();
        console.log(data);
    }
    render() {
        return (
            <div>
                <label for="HCN">Health Card Number</label><br/>
                <input type="text" id="HCN" onChange={this.inputHandle}></input><br />

                <label for="LastName">Last Name</label><br />
                <input type="text" id="LastName" onChange={this.inputHandle}></input><br />

                <label for="FirstName">First Name</label><br />
                <input type="text" id="FirstName" onChange={this.inputHandle}></input><br />

                <label for="MInitial">M Initial</label><br />
                <input type="text" id="MInitial" onChange={this.inputHandle}></input><br />

                <label for="DateBirth">Date of Birth</label><br />
                <input type="text" id="DateBirth" onChange={this.inputHandle}></input><br />

                <label for="Sex">Sex</label><br />
                <input type="text" id="Sex" onChange={this.inputHandle}></input><br />

                <label for="HeadOfHouse">Head Of House</label><br />
                <input type="text" id="HeadOfHouse" onChange={this.inputHandle}></input><br />

                <label for="AddressLine1">AddressLine1</label><br />
                <input type="text" id="AddressLine1" onChange={this.inputHandle}></input><br />

                <label for="AddressLine2">AddressLine2</label><br />
                <input type="text" id="AddressLine2" onChange={this.inputHandle}></input><br />

                <label for="City">City</label><br />
                <input type="text" id="City" onChange={this.inputHandle}></input><br />

                <label for="Province">Province</label><br />
                <input type="text" id="Province" onChange={this.inputHandle}></input><br />

                <label for="PostalCode">Postal Code</label><br />
                <input type="text" id="PostalCode" onChange={this.inputHandle}></input><br />

                <label for="PhoneNumber">Phone Number</label><br />
                <input type="text" id="PhoneNumber" onChange={this.inputHandle}></input><br />

                <input type="button" value="submit" onClick={this.createPatient}></input><br />

            </div>
        )
    }
}