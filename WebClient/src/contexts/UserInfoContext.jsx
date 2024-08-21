import { createContext, useEffect, useState } from 'react';
import { API_GET_USER_INFO } from '../contants';
import { getAsync } from '../axios';

export const UserInfoContext = createContext({});

export const UserInfoContextProvider = ({ children }) => {
    const [userInfo, setUserInfo] = useState(null);

    useEffect(() => {
        (async () => {
            const { isOk, data } = await getAsync(API_GET_USER_INFO);
            isOk && setUserInfo({ ...data });
        })();
    }, []);

    return (
        <UserInfoContext.Provider value={userInfo}>
            {children}
        </UserInfoContext.Provider>
    );
};
