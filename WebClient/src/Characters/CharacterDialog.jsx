import { Formik, useFormikContext } from 'formik';
import { useEffect, useState } from 'react';
import {
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from '@mui/material';
import { getAsync, postAsync, putAsync } from '../axios';
import { API_CHARACTERS, DAILOG_MODE, getCharacterByIdUrl } from '../contants';
import LoadingButton from '../Components/LoadingButton';
import RoomNameTitle from './RoomName';
import CharacterForm from './CharacterForm';
import useNotification from '../hooks/useNotification';

const initValues = {
    name: '',
    description: '',
    storage: null,
};

const CharacterDialogBody = ({ dialogMode, isLoading }) => {
    const { submitForm } = useFormikContext();

    const readOnly = dialogMode === DAILOG_MODE.READ;
    const buttonTitle = dialogMode === DAILOG_MODE.CREATE ? 'Create' : 'Save';
    return (
        <>
            <DialogContent>
                <CharacterForm readOnly={readOnly} />
            </DialogContent>
            {!readOnly && (
                <DialogActions>
                    <LoadingButton
                        caption={buttonTitle}
                        onClick={submitForm}
                        variant='contained'
                        color='mimicSelected'
                        isLoading={isLoading}
                    />
                </DialogActions>
            )}
        </>
    );
};

const CharacterDailog = ({ open, onClose, dialogMode, characterId }) => {
    const [title, setTitle] = useState();
    const [isLoading, setIsLoading] = useState(false);
    const [character, setCharacter] = useState(null);
    const [mode, setMode] = useState(dialogMode);
    const { pushSuccessNotify, pushErrorNotify, clearAllNotification } =
        useNotification();

    const isCreate = mode === DAILOG_MODE.CREATE;

    useEffect(() => {
        setTitle(isCreate ? 'Create Character' : 'Character');
    }, [mode]);

    useEffect(() => {
        if (!characterId || !open) return;

        (async () => {
            setIsLoading(true);
            const { isOk, data } = await getAsync(
                getCharacterByIdUrl(characterId)
            );
            isOk && setCharacter(data);

            setIsLoading(false);
        })();
    }, [open]);

    const handleClose = () => {
        setCharacter(null);
        setMode(dialogMode);
        clearAllNotification();
        onClose?.();
    };

    const createCharacter = async (character) => {
        const { isOk, data } = await postAsync(API_CHARACTERS, character);
        if (isOk) {
            pushSuccessNotify('Персонаж успешно создан');
            setMode(DAILOG_MODE.EDIT);
            setCharacter(data);
        } else {
            pushErrorNotify('Не удалось создать персонажа');
        }
    };

    const changeCharacter = async (character) => {
        const { isOk, data } = await putAsync(
            getCharacterByIdUrl(character.characterId),
            character
        );

        if (isOk) pushSuccessNotify('Персонаж успешно изменен');
        else pushErrorNotify('Не удалось изменить персонажа');
    };

    const handleSubmit = async (values) => {
        setIsLoading(true);

        if (isCreate) await createCharacter(values);
        else await changeCharacter(values);

        setIsLoading(false);
    };

    const canShowRoomNameTitle =
        mode === DAILOG_MODE.READ || mode === DAILOG_MODE.EDIT;

    return (
        <Dialog fullWidth maxWidth={'md'} open={open} onClose={handleClose}>
            <DialogTitle>
                {title}
                {canShowRoomNameTitle && (
                    <>
                        {' '}
                        <RoomNameTitle roomName={character?.roomName} />
                    </>
                )}
            </DialogTitle>
            <Formik
                initialValues={character ?? initValues}
                onSubmit={handleSubmit}
                enableReinitialize
            >
                <CharacterDialogBody
                    dialogMode={mode}
                    isLoading={isLoading}
                    onClose={handleClose}
                />
            </Formik>
        </Dialog>
    );
};

export default CharacterDailog;
