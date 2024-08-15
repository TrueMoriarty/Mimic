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

const initValues = {
    name: '',
    description: '',
};

const CharacterFormDailog = ({ open, onClose, disabled }) => {
    const [isLoading, setIsLoading] = useState(false);

    const handleClose = () => {
        onClose?.();
    };

    const handleSubmit = (values) => {
        console.log(values);
    };

    return (
        <Dialog
            fullWidth={'md'}
            maxWidth={'md'}
            open={open}
            onClose={handleClose}
        >
            {isLoading && (
                <CircularProgress
                    color='mimicLoader'
                    sx={{ mx: 'auto', my: 3 }}
                />
            )}
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
                            <Button
                                type='submit'
                                variant='contained'
                                color='mimicSelected'
                            >
                                Subscribe
                            </Button>
                        </DialogActions>
                    </Form>
                </Formik>
            </DialogContent>
        </Dialog>
    );
};

export default CharacterFormDailog;
