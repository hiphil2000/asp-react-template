import React, {useEffect} from "react";
import useCommonCode from "../../libs/hooks/UseCommonCode";
import {ICommonCode} from "../../libs/apis/Core";

interface IHomeContainerProps {
    
}

const loading = "%%LOADING%%";

export default function HomeContainer({
    
}: IHomeContainerProps) {
    const g001 = useCommonCode("G001");
    const g002 = useCommonCode("G002");
    
    const render = (datas: ICommonCode[]) => {
        return (
            datas.map(data => (
                <option key={data.codeId} value={data.codeId}>{data.codeName}</option>
            ))
        )
    }
    
    return (
        <div>
            <h3>Home</h3>
            <select value={g001.resultList.loading ? loading : g001.value} 
                    onChange={g001.onChange}
            >
                {
                    g001.resultList.loading 
                        ? <option disabled id="%%LOADING%%">Loading...</option> 
                        : ""
                }
                {g001.resultList.data !== null && render(g001.resultList.data)}
            </select>
        </div>
    )
}