import React, {useMemo} from "react";
import {Route, Switch} from "react-router";
import {IRoutedPage, PageList} from "../pages";

export default function PageRoute() {
    // 모든 라우트 정보를 조회합니다.
    const routes = useMemo<IRoutedPage[]>(() => {
        return PageList;
    }, [])

    return (
        <Switch>
            {
                // 모든 라우트 정보를 Route로 만듭니다.
                routes.map(route => <Route key={route.pageId} {...route} />)
            }
        </Switch>
    )
}