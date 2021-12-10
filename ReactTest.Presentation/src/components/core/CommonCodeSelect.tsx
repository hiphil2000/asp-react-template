import React, {ChangeEventHandler, useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {IRootState} from "../../modules";
import {getCommonCodeAsync} from "../../modules/core/get-common-select";
// import {ICommonCode} from "../../libs/apis/core/GetCommonCode";

interface ICodeSelectProps {
    groupId: string;
    value: string;
    
    onChange?: ChangeEventHandler;
}

export default function CommonCodeSelect({
    groupId,
    value,
    onChange
}: ICodeSelectProps) {
    const dispatch = useDispatch();
    const commonCodes = useSelector((state: IRootState) => state.getCommonCodeReducer.data);
    // const [options, setOptions] = useState<ICommonCode[]>([]);
    const [options, setOptions] = useState<any[]>([]);
    
    React.useEffect(() => {
        dispatch(getCommonCodeAsync.request({groupId: groupId}));
    }, [groupId]);
    
    React.useEffect(() => {
        if (commonCodes.data !== null) {
            if (commonCodes.data.groupId === groupId && commonCodes.data.codeList !== null) {
                setOptions(commonCodes.data.codeList);
            }
        }
    }, [commonCodes])
    
    return (
        <select data-groupid={groupId} 
                value={value}
                onChange={onChange}
        >
            {
                options && options.map(option => (
                    <option key={option.codeId} id={option.codeId}>{option.codeName}</option>
                ))
            }
        </select>
    )
}