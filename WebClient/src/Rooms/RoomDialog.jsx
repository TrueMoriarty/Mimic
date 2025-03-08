import {
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
} from '@mui/material';
import { Form, Formik } from 'formik';
import { useState } from 'react';
import LoadingButton from '../Components/LoadingButton';
import TextFieldFormik from '../Components/Formik/TextFieldFormik';
import useNotification from '../hooks/useNotification';
import { postAsync } from '../axios';
import { API_ROOM } from '../contants';

const initRoom = {
    name: '',
};

const RoomDialog = ({ open, onClose }) => {
    const [isLoading, setIsLoading] = useState(false);
    const { notifySuccess, notifyWarning, notifyError } = useNotification();

    const handleClose = () => {
        onClose?.();
    };

    const handleSubmit = async (values) => {
        const form = new FormData();
        form.append('name', values.name);

        const { isOk } = await postAsync(API_ROOM, form);
        if (isOk) {
            notifySuccess('Комната успешно создана');
        } else {
            notifyError('Не удалось создать комнату');
        }
    };

    return (
        <Dialog fullWidth maxWidth={'xs'} open={open} onClose={handleClose}>
            <DialogTitle>Create Room</DialogTitle>
            <Formik
                initialValues={initRoom}
                onSubmit={handleSubmit}
                enableReinitialize
            >
                {(props) => (
                    <DialogContent>
                        <Form>
                            <TextFieldFormik
                                name='name'
                                label='Name'
                                variant='standard'
                                size='small'
                                color='mimicSelected'
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

export default RoomDialog;
