import { Formik, useFormikContext } from 'formik';
import { useEffect, useState } from 'react';
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from '@mui/material';
import { deleteAsync, getAsync, postAsync, putAsync } from '../axios';
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

const CharacterDialogBody = ({ dialogMode, isLoading, onDelete }) => {
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
                    {dialogMode === DAILOG_MODE.EDIT && (
                        <Button
                            onClick={onDelete}
                            variant='contained'
                            color='mimicSelected'
                        >
                            Delete
                        </Button>
                    )}
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
    const { notifySuccess, notifyError, clearAllNotification } =
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
        const characterForm = buildCharacterForm(character);
        const { isOk, data } = await postAsync(API_CHARACTERS, characterForm);
        if (isOk) {
            notifySuccess('Персонаж успешно создан');
            setMode(DAILOG_MODE.EDIT);
            setCharacter(data);
        } else {
            notifyError('Не удалось создать персонажа');
        }
    };

    const changeCharacter = async (character) => {
        const characterForm = buildCharacterForm(character);
        const { isOk, data } = await putAsync(
            getCharacterByIdUrl(character.characterId),
            characterForm
        );

        if (isOk) notifySuccess('Персонаж успешно изменен');
        else notifyError('Не удалось изменить персонажа');
    };

    const handleSubmit = async (values) => {
        setIsLoading(true);

        if (isCreate) await createCharacter(values);
        else await changeCharacter(values);

        setIsLoading(false);
    };

    const handleDelete = async () => {
        const { isOk } = await deleteAsync(
            getCharacterByIdUrl(character.characterId)
        );

        if (isOk) {
            handleClose();
            notifySuccess('Персонаж успешно удален');
        }
    };

    const buildCharacterForm = (character) => {
        const characterForm = new FormData();
        characterForm.append('characterModelJson', JSON.stringify(character));
        characterForm.append('cover', character.cover);
        return characterForm;
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
                    onDelete={handleDelete}
                />
            </Formik>
        </Dialog>
    );
};

export default CharacterDailog;
