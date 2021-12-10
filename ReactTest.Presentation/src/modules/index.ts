import {all} from "redux-saga/effects";
import getCommonCodeReducer, {getCommonCodeSaga, GetCommonCodeState} from "./core/get-common-select";

// 모든 리듀서의 목록입니다.
export const reducers = {
    // Begin Cores
    getCommonCodeReducer
    // End Cores
}

// 스토어의 RootState 인터페이스입니다.
export interface IRootState {
    // Begin Cores
    getCommonCodeReducer: GetCommonCodeState;
    // End Cores
}

export function* rootSaga() {
    yield all([
        // Begin Core Saga
        getCommonCodeSaga(),
        // End Core Saga
    ]);
}