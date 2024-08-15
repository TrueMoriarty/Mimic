import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.jsx';
import { BrowserRouter } from 'react-router-dom';
import { ThemeProvider } from '@mui/material';
import { mimicTheme } from './mimicTheme.js';
import './index.css';
import { UserInfoContextProvider } from './contexts/UserInfoContext.jsx';

ReactDOM.createRoot(document.getElementById('root')).render(
    <ThemeProvider theme={mimicTheme}>
        <BrowserRouter>
            <UserInfoContextProvider>
                <App />
            </UserInfoContextProvider>
        </BrowserRouter>
    </ThemeProvider>
);
