import React from 'react';
import {BrowserRouter, Link} from 'react-router-dom';
import PageRoute from "./libs/PageRoute";
import {HomeInfo, TestPageInfo} from "./pages";
import NavBar from "./components/layout/NavBar";

function App() {
    return (
        <BrowserRouter>
            <NavBar>
                <Link to={HomeInfo.path}>Home</Link>
                <Link to={TestPageInfo.path}>TestPage</Link>
            </NavBar>
            <PageRoute/>
        </BrowserRouter>
    );
}

export default App;
