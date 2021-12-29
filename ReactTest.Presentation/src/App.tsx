import React from 'react';
import {BrowserRouter} from 'react-router-dom';
import PageRoute from "./libs/PageRoute";
import {ThemeProvider} from "@mui/material";
import theme from "./libs/mui/Theme";

function App() {
    
    
    return (
        <ThemeProvider theme={theme}>
            <BrowserRouter>
                <PageRoute/>
            </BrowserRouter>
        </ThemeProvider>
    );
}

export default App;
