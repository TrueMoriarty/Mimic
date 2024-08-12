const BACK_URL = 'https://localhost/api/';

//API

//Characters
export const GET_CREATOR_CHARACTERS = BACK_URL + 'characters/creator';
export const getCharacterById = (id) => BACK_URL + `characters/${id}`;
