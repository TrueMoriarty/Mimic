import axios from "axios";
import { useState } from "react";

function App() {
  const [userId, setUserId] = useState(null);

  const handleAuth = async () =>
    window.location.assign("https://localhost/api/auth/oidc/vk");

  const handleInfo = async () => {
    axios.defaults.withCredentials = true;
    const response = await axios.get("https://localhost/api/auth/oidc/userId");
    setUserId(response.data);
  };

  return (
    <>
      <div>
        <span>{userId}</span>
      </div>
      <button onClick={handleAuth}>Vk auth</button>
      <button onClick={handleInfo}>Info</button>
    </>
  );
}

export default App;
