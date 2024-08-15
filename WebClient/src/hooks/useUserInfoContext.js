import { useContext } from 'react';
import { UserInfoContext } from '../contexts/UserInfoContext';

const useUserInfoContext = () => {
    return useContext(UserInfoContext);
};
export default useUserInfoContext;
