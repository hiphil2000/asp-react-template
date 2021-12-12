# ReactTest.Presentation

## Quick Start
1. `yarn` 명령어를 실행하여 Dependency 설치
2. `yarn start` 명령어를 실행하여 Dev Server 시작
3. `yarn run build` 명령어를 실행하여 Project Build

## API Fetch 관련
일반적인 경우에는 Redux/Redux-Saga를 사용하여 API Fetch를 진행하고, Redux를 사용하기 힘든 경우에는 `/src/libs/hooks/UseFetch.ts` 를 사용한다.

### UseFetch
내부적으로 `React.useReducer` 를 사용하는 Hook이며, Redux를 사용하지 않는 Async Fetch 작업에 사용된다.
```typescript jsx
// 예제(/src/libs/hooks/UseCommonCode.ts)
import useFetch from "./UseFetch";
import {GetCommonCodes, ICommonCode} from "../apis/Core";
import {useEffect} from "react";

export default function useCommonCode(groupId: string) {
    const [state, fetch] = useFetch<string, ICommonCode[]>(GetCommonCodes);
    
    useEffect(() => {
        fetch(groupId);
    }, [fetch, groupId]);
    
    return [state];
}
```