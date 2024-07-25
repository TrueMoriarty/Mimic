import Home from "./Components/Home/Home";
import Navigation from "./Components/navigation";
import { Route, Routes } from "react-router-dom";
import { Unbording } from "./Components/User/Unbording";
import Login from "./Components/User/Login";
import ItemsLibrary from "./Components/ItemsLibrary/ItemsLibrary";
import Rooms from "./Components/Rooms/Rooms";

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
