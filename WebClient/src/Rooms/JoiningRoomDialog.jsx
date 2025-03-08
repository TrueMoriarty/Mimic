import {
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
} from '@mui/material';
import { Form, Formik } from 'formik';
import TextFieldFormik from '../Components/Formik/TextFieldFormik';
import LoadingButton from '../Components/LoadingButton';
import { API_CHARACTERS, API_ROOM } from '../contants';
import { getAsync, postAsync } from '../axios';
import { useEffect, useState } from 'react';
import useNotification from '../hooks/useNotification';
import AutocompliteFormik from '../Components/Formik/AutocompliteFormik';

const joinedRoom = {
    roomId: '',
    character: null,
};
const JoiningRoomDialog = ({ open, onClose }) => {
    const [isLoading, setIsLoading] = useState(false);
    const { notifySuccess, notifyWarning, notifyError } = useNotification();

    const [value, setValue] = useState(null);
    const [inputValue, setInputValue] = useState('');
    const [options, setOptions] = useState([]);
    const [loadedOptions, setLoadedOptions] = useState(false);

    const handleClose = () => {
        onClose?.();
    };

    const handleSubmit = async (values) => {
        const form = new FormData();
        form.append('characterId', value.characterId);

        const { isOk } = await postAsync(
            API_ROOM + `/${values.roomId}/join`,
            form
        );
        if (isOk) {
            notifySuccess('Удалось войти в комнату');
        } else {
            notifyError('Не удалось войти в комнату');
        }
    };

    useEffect(() => {
        if (loadedOptions) return;
        if (inputValue === '') return;

        (async () => {
            setLoadedOptions(true);
            const { isOk, data } = await getAsync(
                API_CHARACTERS + `/suggests?query=${inputValue}`
            );
            if (isOk) setOptions(data);
            setLoadedOptions(false);
        })();
    }, [inputValue]);

    return (
        <Dialog fullWidth maxWidth={'xs'} open={open} onClose={handleClose}>
            <DialogTitle>Join room</DialogTitle>
            <Formik initialValues={joinedRoom} onSubmit={handleSubmit}>
                {(props) => (
                    <DialogContent>
                        <Form>
                            <TextFieldFormik
                                name='roomId'
                                label='RoomId'
                                variant='standard'
                                size='small'
                                color='mimicSelected'
                            />
                            <AutocompliteFormik
                                name='character'
                                label='Character'
                                options={options}
                                autoComplete
                                value={value}
                                loading={loadedOptions}
                                onChange={(event, newValue) => {
                                    setOptions(
                                        newValue
                                            ? [newValue, ...options]
                                            : options
                                    );
                                    setValue(newValue);
                                }}
                                onInputChange={(event, newInputValue) => {
                                    setInputValue(newInputValue);
                                }}
                                getOptionLabel={(option) =>
                                    typeof option === 'string'
                                        ? option
                                        : option.name
                                }
                                renderInput={(params) => (
                                    <TextField
                                        {...params}
                                        label='Character'
                                        fullWidth
                                    />
                                )}
                            />
                        </Form>
                        <DialogActions>
                            <LoadingButton
                                caption={'submit'}
                                onClick={props.handleSubmit}
                                variant='contained'
                                color='mimicSelected'
                                isLoading={isLoading}
                            />
                        </DialogActions>
                    </DialogContent>
                )}
            </Formik>
        </Dialog>
    );
};

export default JoiningRoomDialog;
