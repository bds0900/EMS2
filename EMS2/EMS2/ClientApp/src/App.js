import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import  Demography  from './components/Demography/Demography';

import './custom.css'
import { SearchPatient } from './components/Demography/SearchPatient';
import { CreatePatient } from './components/Demography/CreatePatient';
import { UpdatePatient } from './components/Demography/UpdatePatient';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/home' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
            <Route exact path='/demography' component={Demography} />
        <Route path='/demography/add' component={CreatePatient} />
        <Route path='/demography/search' component={SearchPatient} />
        <Route path='/demography/update/:id' component={UpdatePatient} />
      </Layout>
    );
  }
}
