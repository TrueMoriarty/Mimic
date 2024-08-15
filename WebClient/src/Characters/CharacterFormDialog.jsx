import { Form, Formik } from 'formik';
import React, { useState } from 'react';
import TextFieldFormik from '../Components/Formik/TextFieldFormik';
import {
    Button,
    CircularProgress,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Grid,
    Stack,
} from '@mui/material';
import ImageBox from '../Components/ImageBox';
import { postAsync } from '../axios';
import { API_CHARACTERS } from '../contants';
import LoadingButton from '../Components/LoadingButton';

const initValues = {
    name: '',
    description: '',
};

const CharacterFormDailog = ({ open, onClose, disabled }) => {
    const [isLoading, setIsLoading] = useState(false);

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
            <DialogTitle>Create Character</DialogTitle>
            <DialogContent>
                <Formik initialValues={initValues} onSubmit={handleSubmit}>
                    <Form>
                        <Grid container spacing={1}>
                            <Grid item xs={12} md={4}>
                                <ImageBox />
                            </Grid>
                            <Grid item xs={12} md={8}>
                                <Stack direction='column' spacing={1}>
                                    <TextFieldFormik
                                        name='name'
                                        label='Name'
                                        variant='standard'
                                        disabled={disabled}
                                        size='small'
                                        color='mimicSelected'
                                    />
                                    <TextFieldFormik
                                        name='description'
                                        label='Description'
                                        variant='standard'
                                        size='small'
                                        color='mimicSelected'
                                        disabled={disabled}
                                        multiline
                                        maxRows={3}
                                    />
                                </Stack>
                            </Grid>
                        </Grid>
                        <DialogActions>
                            <Button
                                onClick={handleClose}
                                variant='contained'
                                color='mimicSelected'
                            >
                                Cancel
                            </Button>
                            <LoadingButton
                                caption={'Subscribe'}
                                type='submit'
                                variant='contained'
                                color='mimicSelected'
                                isLoading={isLoading}
                            />
                        </DialogActions>
                    </Form>
                </Formik>
            </DialogContent>
        </Dialog>
    );
};

export default CharacterFormDailog;
