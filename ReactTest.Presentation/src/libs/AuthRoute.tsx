import React, {useEffect} from "react";
import {Redirect, Route, RouteComponentProps} from "react-router";
import useAuth from "./hooks/UseAuth";
import {Typography} from "@mui/material";
import {LoginInfo} from "../pages/auth";

interface IAuthRouteProps {
    roles: string[];
    path: string;
    component: React.ComponentType<RouteComponentProps<any>> | React.ComponentType<any> | undefined;
    exact?: boolean;
}

/**
 * 인증 여부를 판단하여 라우팅을 진행합니다.
 * 인증되어있지 않다면 @redirectTo 로 리다이렉트합니다.
 * 
 * @param authenticated 인증 여부
 * @param redirectTo 리다이렉트 할 주소
 * @param path 실제 라우팅 주소
 * @param component 라우팅 대상 컴포넌트
 * @param exact 주소 정확 여부 (react-router)
 * @constructor
 */
export default function AuthRoute({
    roles,
    path,
    component,
    exact
}: IAuthRouteProps) {
    const {state, validateToken} = useAuth();
    
    useEffect(() => {
        validateToken();
    }, []);
    
    const render = () => {
        if (state.loading || state.data == null) {
            return (<Typography>Loading...</Typography>)
        } else {
            if (roles.find(r => r === state.data?.role)) {
                return (
                    <Route path={path} component={component} exact={exact} />
                )
            } else {
                return (
                    <Redirect to={LoginInfo.path} />
                )
            }
        }
    }
    
    return render();
}