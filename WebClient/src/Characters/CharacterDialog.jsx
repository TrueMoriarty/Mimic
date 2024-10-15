import { Formik, useFormikContext } from 'formik';
import React, { useEffect, useState } from 'react';
import {
    Box,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    IconButton,
    Stack,
} from '@mui/material';
import { getAsync, postAsync, putAsync } from '../axios';
import { API_CHARACTERS, DAILOG_MODE, getCharacterByIdUrl } from '../contants';
import LoadingButton from '../Components/LoadingButton';
import RoomNameTitle from './RoomName';
import CharacterForm from './CharacterForm';

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

const CharacterDailog = ({ title, open, onClose, dialogMode, characterId }) => {
    const [isLoading, setIsLoading] = useState(false);
    const [character, setCharacter] = useState(null);
    const [mode, setMode] = useState(dialogMode);

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
        onClose?.();
    };

    const handleSubmit = async (values) => {
        setIsLoading(true);

        const isCreate = mode === DAILOG_MODE.CREATE;
        const httpMethod = isCreate ? postAsync : putAsync;
        const url = isCreate
            ? API_CHARACTERS
            : getCharacterByIdUrl(values.characterId);

        const { isOk, data } = await httpMethod(url, values);

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
