import { Formik, useFormikContext } from 'formik';
import React, { useEffect, useState } from 'react';
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from '@mui/material';
import { getAsync, postAsync } from '../axios';
import { API_CHARACTERS, getCharacterByIdURL } from '../contants';
import LoadingButton from '../Components/LoadingButton';
import RoomNameTitle from './RoomName';
import CharacterForm from './CharacterForm';

const initValues = {
    name: '',
    description: '',
    storage: null,
};

const CharacterDialogBody = ({ readOnly, onClose, isLoading }) => {
    const { submitForm } = useFormikContext();
    return (
        <>
            <DialogContent>
                <CharacterForm readOnly={readOnly} />
            </DialogContent>
            {!readOnly && (
                <DialogActions>
                    <Button
                        onClick={onClose}
                        variant='contained'
                        color='mimicSelected'
                    >
                        Cancel
                    </Button>
                    <LoadingButton
                        caption={'Add'}
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

const CharacterDailog = ({ title, open, onClose, readOnly, characterId }) => {
    const [isLoading, setIsLoading] = useState(false);
    const [character, setCharacter] = useState(null);

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
        onClose?.();
    };

    const handleSubmit = async (values) => {
        setIsLoading(true);
        const { isOk, data } = await postAsync(API_CHARACTERS, values);
        setIsLoading(false);
    };

    return (
        <Dialog
            fullWidth={'md'}
            maxWidth={'md'}
            open={open}
            onClose={handleClose}
        >
            <DialogTitle>
                {title}
                {readOnly && (
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
                    readOnly={readOnly}
                    isLoading={isLoading}
                    onClose={handleClose}
                />
            </Formik>
        </Dialog>
    );
};

export default CharacterDailog;
