import React, {useEffect} from "react";
import useCommonCode from "../../libs/hooks/UseCommonCode";
import {ICommonCode} from "../../libs/apis/Core";

interface IHomeContainerProps {
    
}

export default function HomeContainer({
    
}: IHomeContainerProps) {
    const [g001] = useCommonCode("G001");
    const [g002] = useCommonCode("G002");
    
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
            <select>
                {
                    g001.loading 
                        ? <option disabled selected>Loading...</option> 
                        : ""
                }
                {g001.data !== null && render(g001.data)}
            </select>
            {/*<CommonCodeSelect {...g1_code}/>*/}
            {/*<CommonCodeSelect {...g2_code}/>*/}
            {/*<CommonCodeSelect {...g1_code2}/>*/}
        </div>
    )
}