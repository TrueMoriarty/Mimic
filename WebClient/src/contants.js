//todo: исправить это. Не нужно хранить url сервера на клиенте
export const BACK_URL = 'https://localhost/api/';

//API

//Users
export const API_GET_USER_INFO = BACK_URL + 'users/info';

//Characters
export const API_CHARACTERS = BACK_URL + 'characters';
export const API_GET_PAGED_CHARACTERS = BACK_URL + 'characters/page';
export const getCharacterByIdUrl = (id) => BACK_URL + `characters/${id}`;

//Items
export const API_ITEMS = BACK_URL + 'items';
export const getItemSuggestsUrl = (query, creatorId) =>
    BACK_URL + `items/suggests?query=${query}&creatorId=${creatorId}`;

export const getItemByIdUrl = (id) => BACK_URL + `items/${id}`;

//Rooms
export const API_ROOM = BACK_URL + 'rooms';
export const API_GET_PAGED_ROOMS = BACK_URL + 'rooms/page';
// ENUMS
export const DAILOG_MODE = {
    READ: 1,
    EDIT: 2,
    CREATE: 3,
};
