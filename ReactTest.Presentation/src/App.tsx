import React from 'react';
import {BrowserRouter, Link} from 'react-router-dom';
import PageRoute from "./libs/PageRoute";
import {HomeInfo, TestPageInfo} from "./pages";

function App() {
    return (
        <BrowserRouter>
            <Link to={HomeInfo.path}>Home</Link>
            <Link to={TestPageInfo.path}>TestPage</Link>
            <PageRoute/>
        </BrowserRouter>
    );
}

export default App;
