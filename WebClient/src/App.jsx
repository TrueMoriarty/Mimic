import Home from "./Home/Home";
import Navigation from "./navigation";
import { Route, Routes } from "react-router-dom";
import { Unbording } from "./User/Unbording";
import Login from "./User/Login";
import ItemsLibrary from "./ItemsLibrary/ItemsLibrary";
import Rooms from "./Rooms/Rooms";

function App() {
  return (
    <>
      <Navigation />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/rooms" element={<Rooms />} />
        <Route path="/itemsLibrary" element={<ItemsLibrary />} />
        <Route path="/user/login" element={<Login />} />
        <Route path="/user/unbording" element={<Unbording />} />
      </Routes>
    </>
  );
}

export default App;
