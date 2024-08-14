import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import { BrowserRouter } from "react-router-dom";
import { ThemeProvider } from "@mui/material";
import { mimicTheme } from "./mimicTheme.js";
import './index.css'


ReactDOM.createRoot(document.getElementById("root")).render(
  <ThemeProvider theme={mimicTheme}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </ThemeProvider>
);
