import React, { Component } from 'react';

export class UpdatePatient extends Component {
    constructor(props) {
        super(props);
        this.state = {
            hcn: "",
            lastName: "",
            firstName: "",
            mInitial: "",
            dateBirth: "",
            sex: "",
            headOfHouse: "",
            addressLine1: "",
            addressLine2: "",
            city: "",
            province: "",
            postalCode: "",
            phoneNumber: ""
        }
        this.inputHandle = this.inputHandle.bind(this);
        this.updatePatient = this.updatePatient.bind(this);

    }
    async componentDidMount() {
        const response = await fetch(`/api/patients/${this.props.match.params.id}`)
        const data = await response.json();
        console.log(data);
        Object.entries(data).forEach(obj => {
            console.log(obj[0] + obj[1]);
            document.getElementById(obj[0]).value = obj[1];
            this.setState({ [obj[0]]: obj[1] });
        })
        
    }

    inputHandle(event) {
        this.setState({
            [event.target.id]: event.target.value
        })
    }
    async updatePatient() {
        const response = await fetch(`/api/patients/${this.state.hcn}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.state)
        })
        //const data = await response.json();
        console.log(response);
    }
    render() {
        return (
            <div>
                <label for="hcn">Health Card Number</label><br />
                <input type="text" id="hcn" disabled onChange={this.inputHandle}></input><br />

                <label for="lastName">Last Name</label><br />
                <input type="text" id="lastName" onChange={this.inputHandle}></input><br />

                <label for="firstName">First Name</label><br />
                <input type="text" id="firstName" onChange={this.inputHandle}></input><br />

                <label for="mInitial">M Initial</label><br />
                <input type="text" id="mInitial" onChange={this.inputHandle}></input><br />

                <label for="dateBirth">Date of Birth</label><br />
                <input type="text" id="dateBirth" onChange={this.inputHandle}></input><br />

                <label for="sex">Sex</label><br />
                <input type="text" id="sex" onChange={this.inputHandle}></input><br />

                <label for="headOfHouse">Head Of House</label><br />
                <input type="text" id="headOfHouse" onChange={this.inputHandle}></input><br />

                <label for="addressLine1">AddressLine1</label><br />
                <input type="text" id="addressLine1" onChange={this.inputHandle}></input><br />

                <label for="addressLine2">AddressLine2</label><br />
                <input type="text" id="addressLine2" onChange={this.inputHandle}></input><br />

                <label for="city">City</label><br />
                <input type="text" id="city" onChange={this.inputHandle}></input><br />

                <label for="province">Province</label><br />
                <input type="text" id="province" onChange={this.inputHandle}></input><br />

                <label for="postalCode">Postal Code</label><br />
                <input type="text" id="postalCode" onChange={this.inputHandle}></input><br />

                <label for="phoneNumber">Phone Numer</label><br />
                <input type="text" id="phoneNumber" onChange={this.inputHandle}></input><br />

                <input type="button" value="submit" onClick={this.updatePatient}></input><br />

            </div>
        )
    }
}