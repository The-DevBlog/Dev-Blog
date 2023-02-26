import { Route, Routes } from 'react-router-dom';
import Nav from "./components/Nav";
import SignOut from './pages/SignOut';
import About from './pages/About';
import CreatePost from './pages/CreatePost';
import Home from './pages/Home';
import SignIn from './pages/SignIn';
import Posts from './pages/Posts';
import SignUp from './pages/SignUp';

const App = () => {
  return (
    <>
      <Nav />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/posts" element={<Posts />} />
        <Route path="/about" element={<About />} />
        <Route path="/posts/create" element={<CreatePost />} />
        <Route path="/signin" element={<SignIn />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/signout" element={<SignOut />} />
      </Routes>
    </>
  );
}

export default App;