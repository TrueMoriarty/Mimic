//todo: исправить это. Не нужно хранить url сервера на клиенте
const BACK_URL = 'https://localhost/api/';

//API

//Users
export const GET_USER_INFO = BACK_URL + 'users/info';

//Characters
export const GET_PAGED_CHARACTERS = BACK_URL + 'characters/page';
export const getCharacterById = (id) => BACK_URL + `characters/${id}`;
