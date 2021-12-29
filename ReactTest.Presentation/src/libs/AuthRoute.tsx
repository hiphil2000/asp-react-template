import React from "react";
import {Redirect, Route, RouteComponentProps} from "react-router";

interface IAuthRouteProps {
    authenticated?: boolean;
    redirectTo?: string;
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