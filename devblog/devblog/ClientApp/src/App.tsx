import { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import Nav from "./components/Nav";
import Home from './pages/Home';
import Posts from './pages/Posts';

export default class App extends Component {
  render() {
    return (
      <>
        <Nav />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/posts" element={<Posts />} />
        </Routes>
      </>
    );
  }
}