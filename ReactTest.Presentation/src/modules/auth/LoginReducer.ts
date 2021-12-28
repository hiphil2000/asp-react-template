import {ActionType, createAsyncAction, createReducer} from "typesafe-actions";
import Login, {ILoginPayload, ILoginResponse} from "../../libs/apis/Auth";
import {AxiosError} from "axios";
import {asyncActionToArray, AsyncState, asyncStateHelper, createAsyncReducer} from "../ReducerUtils";
import {call, put, takeLatest} from "redux-saga/effects";
import {IRootState} from "../index";

// Actions
const REQUEST_LOGIN = "auth/REQUEST_LOGIN";
const REQUEST_LOGIN_SUCCESS = "auth/REQUEST_LOGIN_SUCCESS";
const REQUEST_LOGIN_ERROR = "auth/REQUEST_LOGIN_ERROR";

export const loginAsync = createAsyncAction(
    REQUEST_LOGIN,
    REQUEST_LOGIN_SUCCESS,
    REQUEST_LOGIN_ERROR
)<ILoginPayload, ILoginResponse, AxiosError>();

// Types
export type ILoginAction = ActionType<typeof loginAsync>;
export type ILoginState = {
    data: AsyncState<ILoginResponse, AxiosError>
};

// Reducer
const initialState: ILoginState = {
    data: asyncStateHelper.initial()
};

const loginReducer = createReducer<ILoginState, ILoginAction>(initialState).handleAction(
    asyncActionToArray(loginAsync),
    createAsyncReducer(loginAsync, 'data')
);

export default loginReducer;

// Saga
function* _loginSaga(action: ReturnType<typeof loginAsync.request>) {
    try {
        const result: ILoginResponse = yield call(Login, action.payload);
        yield put(loginAsync.success(result));
    } catch (e) {
        yield put(loginAsync.failure(e as AxiosError));
    }
}

export function* loginSaga() {
    yield takeLatest(REQUEST_LOGIN, _loginSaga);
}

// Selectors
export const loginSelector = (state: IRootState) => state.LoginReducer.data;