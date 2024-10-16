import axios from 'axios';

const wrapAction = async (action, ...props) => {
    let response = null;

    try {
        axios.defaults.withCredentials = true;
        response = await action(...props);
    } catch (e) {
        return { isOk: false, data: e };
    }

    return { isOk: true, data: response.data };
};

export const getAsync = (...props) => wrapAction(axios.get, ...props);
export const postAsync = (...props) => wrapAction(axios.post, ...props);
export const putAsync = (...props) => wrapAction(axios.put, ...props);
export const deleteAsync = (...props) => wrapAction(axios.delete, ...props);
