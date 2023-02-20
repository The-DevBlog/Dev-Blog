import { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import Nav from "./components/Nav";
import About from './pages/About';
import CreatePost from './pages/CreatePost';
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
          <Route path="/about" element={<About />} />
          <Route path="/posts/create" element={<CreatePost />} />
        </Routes>
      </>
    );
  }
}