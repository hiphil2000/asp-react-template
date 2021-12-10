import {createReducer} from "typesafe-actions";
import {GetCommonCodeAction, GetCommonCodeState} from "./Types";
import {asyncActionToArray, asyncStateHelper, createAsyncReducer} from "../../ReducerUtils";
import {getCommonCodeAsync} from "./Actions";

const initialState: GetCommonCodeState = {
    data: asyncStateHelper.initial()
};

const getCommonCodeReducer = createReducer<GetCommonCodeState, GetCommonCodeAction>(initialState)
    .handleAction(
        asyncActionToArray(getCommonCodeAsync),
        createAsyncReducer(getCommonCodeAsync, "data")
    );

export default getCommonCodeReducer;