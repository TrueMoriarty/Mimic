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
import { API_CHARACTERS, DAILOG_MODE, getCharacterByIdURL } from '../contants';
import LoadingButton from '../Components/LoadingButton';
import RoomNameTitle from './RoomName';
import CharacterForm from './CharacterForm';
import EditIcon from '@mui/icons-material/Edit';

const initValues = {
    name: '',
    description: '',
    storage: null,
};

const CharacterDialogBody = ({ dialogMode, isLoading }) => {
    const { submitForm } = useFormikContext();

    const readOnly = dialogMode === DAILOG_MODE.READ;
    const buttonTitle = dialogMode === DAILOG_MODE.CREATE ? 'Add' : 'Edit';
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
                getCharacterByIdURL(characterId)
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
            : API_CHARACTERS + `/${values.characterId}`;

        const { isOk, data } = await httpMethod(url, values);

        setIsLoading(false);
    };

    const handleSetEditMode = () => {
        setMode(DAILOG_MODE.EDIT);
    };

    const canShowRoomNameTitle =
        mode === DAILOG_MODE.READ || mode === DAILOG_MODE.EDIT;

    return (
        <Dialog
            fullWidth={'md'}
            maxWidth={'md'}
            open={open}
            onClose={handleClose}
        >
            <DialogTitle>
                <Stack
                    direction='row'
                    spacing={2}
                    sx={{
                        justifyContent: 'space-between',
                        alignItems: 'center',
                    }}
                >
                    <Box>
                        {title}
                        {canShowRoomNameTitle && (
                            <>
                                {' '}
                                <RoomNameTitle roomName={character?.roomName} />
                            </>
                        )}
                    </Box>
                    {mode === DAILOG_MODE.READ && (
                        <IconButton
                            aria-label='edit'
                            onClick={handleSetEditMode}
                        >
                            <EditIcon />
                        </IconButton>
                    )}
                </Stack>
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
