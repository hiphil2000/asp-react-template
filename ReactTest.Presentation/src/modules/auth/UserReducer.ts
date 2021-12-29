import {ActionType, createAsyncAction, createReducer} from "typesafe-actions";
import {AxiosError} from "axios";
import {IUser} from "../../libs/apis/Interfaces";
import {asyncActionToArray, AsyncState, asyncStateHelper, createAsyncReducer} from "../ReducerUtils";
import {call, put, takeLatest} from "redux-saga/effects";
import {GetCurrentUser, IGetCurrentUserResponse} from "../../libs/apis/Auth";

const GET_USER = "auth/GET_USER";
const GET_USER_SUCCESS = "auth/GET_USER_SUCCESS";
const GET_USER_ERROR = "auth/GET_USER_ERROR";

/**
 * 유저 리듀서의 액션 생성자입니다.
 */
export const getUserAsync = createAsyncAction(
    GET_USER,
    GET_USER_SUCCESS,
    GET_USER_ERROR
)<void, IGetCurrentUserResponse, AxiosError>();

/**
 * 유저 리듀서의 액션 타입입니다.
 */
export type UserAction = ActionType<typeof getUserAsync>;
/**
 * 유저 리듀서의 상태 타입입니다.
 */
export type UserState = {
    data: AsyncState<UserAction, AxiosError>
}

/**
 * 리듀서의 기본 값입니다.
 */
const initialState: UserState = {
    data: asyncStateHelper.initial()
}

/**
 * 유저 리듀서 입니다.
 */
const userReducer = createReducer<UserState, UserAction>(initialState).handleAction(
    asyncActionToArray(getUserAsync),
    createAsyncReducer(getUserAsync, 'data')
)

export default userReducer;

/**
 * 유저 사가입니다.
 * @param action
 */
function* _userSaga(action: ReturnType<typeof getUserAsync.request>) {
    try {
        const result: IGetCurrentUserResponse = yield call(GetCurrentUser);
        yield put(getUserAsync.success(result));
    } catch (e) {
        yield put(getUserAsync.failure(e as AxiosError))
    }
}

/**
 * 유저 사가 래퍼 함수 입니다.
 */
export function* userSaga() {
    yield takeLatest(GET_USER, _userSaga);
}