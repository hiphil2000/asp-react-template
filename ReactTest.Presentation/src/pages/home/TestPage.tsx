import React from "react";
import {IRoutedPage} from "../Interfaces";

/**
 * 테스트 페이지입니다.
 * @constructor
 */
export default function TestPage() {
    return (
        <div>
            <h3>TestPage</h3>
        </div>
    )
}

/**
 * 테스트 페이지의 라우트 정보입니다.
 */
export const TestPageInfo: IRoutedPage = {
    pageId: "HOME_TEST",
    pageName: "테스트 페이지",
    path: "/test-page",
    exact: true,
    component: TestPage
};