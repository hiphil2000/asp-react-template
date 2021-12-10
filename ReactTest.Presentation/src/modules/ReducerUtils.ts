import {AsyncActionCreatorBuilder, getType} from "typesafe-actions";
import {AnyAction} from "redux";

/**
 * Async 상태 타입 입니다.
 */
export type AsyncState<TData, Error = any> = {
    data: TData | null;
    loading: boolean;
    error: Error | null;
}

/**
 * Async Reducer 상태 헬퍼입니다.
 */
export const asyncStateHelper = {
    initial: <T, E = any>(initialData?: T): AsyncState<T, E> => ({
        loading: false,
        data: initialData || null,
        error: null
    }),
    load: <T, E = any>(data?: T): AsyncState<T, E> => ({
        loading: true,
        data: data || null,
        error: null
    }),
    success: <T, E = any>(data: T): AsyncState<T, E> => ({
        loading: false,
        data,
        error: null
    }),
    error: <T, E>(error: E): AsyncState<T, E> => ({
        loading: false,
        data: null,
        error: error
    })
};

/**
 * AsyncActionCreatorBuilder<any, any, any>의 Shortcut입니다.
 */
type AnyAsyncActionCreator = AsyncActionCreatorBuilder<any, any, any>;

/**
 * AsyncActionCreator의 Action을 배열로 변형하여 반환합니다.
 * @param asyncActionCreator
 */
export function asyncActionToArray<AC extends AnyAsyncActionCreator>(asyncActionCreator: AC) {
    const {request, success, failure} = asyncActionCreator;
    return [request, success, failure];
}

/**
 * 비동기 처리 리듀서를 생성합니다.
 * @param asyncActionCreator createAsyncAction로 생성된 Action Creator
 * @param key
 */
export function createAsyncReducer<S, AC extends AnyAsyncActionCreator, K extends keyof S>(
    asyncActionCreator: AC,
    key: K
) {
    // 리듀서를 생성하여 반환합니다.
    return (state: S, action: AnyAction) => {
        // 각 Action Creator의 Type을 추출합니다.
        const [request, success, failure] = asyncActionToArray(asyncActionCreator).map(getType);

        // Action 타입에 따라 헬퍼를 사용합니다.
        switch (action.type) {
            case request:
                return {
                    ...state,
                    [key]: asyncStateHelper.load()
                };
            case success:
                return {
                    ...state,
                    [key]: asyncStateHelper.success(action.payload)
                };
            case failure:
                return {
                    ...state,
                    [key]: asyncStateHelper.error(action.payload)
                };
            default:
                return state;
        }
    }
}
