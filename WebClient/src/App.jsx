import Home from "./Home/Home";
import Navigation from "./navigation";
import { Route, Routes } from "react-router-dom";
import { Unbording } from "./User/Unbording";
import Login from "./User/Login";
import Library from "./Library/Library";
import Rooms from "./Rooms/Rooms";
import Workshop from "./Workshop/Workshop";
import Characters from "./Characters/Characters";

function App() {
  return (
    <>
      <Navigation />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/rooms" element={<Rooms />} />
        <Route path="/library" element={<Library />} />
        <Route path="/workshop" element={<Workshop />} />
        <Route path="/characters" element={<Characters />} />
        <Route path="/user/login" element={<Login />} />
        <Route path="/user/unbording" element={<Unbording />} />
      </Routes>
    </>
  );
}

export default App;
