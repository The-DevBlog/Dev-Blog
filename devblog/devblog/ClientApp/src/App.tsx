import { Route, Routes } from 'react-router-dom';
import Nav from "./components/Nav";
import Footer from './components/Footer';
import SignOut from './pages/SignOut';
import AddPost from './pages/AddPost';
import Home from './pages/Home';
import SignIn from './pages/SignIn';
import Posts from './pages/Posts';
import SignUp from './pages/SignUp';
import About from './pages/About';
import Insights from './pages/Insights';
import Account from './pages/Account';
import "./global.css";

const App = () => {
  return (
    <>
      <Nav />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/posts" element={<Posts />} />
        <Route path="/posts/create" element={<AddPost />} />
        <Route path="/about" element={<About />} />
        <Route path="/account" element={<Account />} />
        <Route path="/insights" element={<Insights />} />
        <Route path="/signin" element={<SignIn />} />
        <Route path="/signup" element={<SignUp />} />
        <Route path="/signout" element={<SignOut />} />
      </Routes>
      <Footer />
    </>
  );
}

export default App;