import { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import Nav from "./components/Nav";
import Home from './pages/Home';

export default class App extends Component {
  render() {
    return (
      <>
        <Nav />
        <Routes>
          <Route path="/" element={<Home />} />
        </Routes>
      </>
    );
  }
}