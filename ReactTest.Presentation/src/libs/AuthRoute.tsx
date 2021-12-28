import React from "react";
import {Redirect, Route} from "react-router";

interface IAuthRouteProps {
    authenticated?: boolean;
    redirectTo?: string;
    path: string;
    component: any;
    exact?: boolean;
}

export default function AuthRoute({
    authenticated,
    redirectTo,
    path,
    component,
    exact
}: IAuthRouteProps) {
    return (
        authenticated ?
            <Route path={path} component={component} exact={exact} /> :
            <Redirect to={redirectTo} />
    )
}