import { Form, Formik, useFormikContext } from 'formik';
import React, { useEffect, useState } from 'react';
import TextFieldFormik from '../Components/Formik/TextFieldFormik';
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Grid,
    IconButton,
    Stack,
} from '@mui/material';
import ImageBox from '../Components/ImageBox';
import { getAsync, postAsync } from '../axios';
import { API_CHARACTERS } from '../contants';
import LoadingButton from '../Components/LoadingButton';
import ItemAutocomplete from '../Items/ItemAutocomplete';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import ItemAccordion from '../Items/ItemAccordion';

const initValues = {
    name: '',
    description: '',
    items: [],
};

const CharacterAddedItemForm = ({}) => {
    const [selectedItem, setSelectedItem] = useState(null);
    const { values, setFieldValue } = useFormikContext();

    const handleAddItem = async () => {
        //const { isOk, data } = await getAsync();
        const newItems = [selectedItem, ...values.items];
        setFieldValue('items', newItems);
    };

    return (
        <Grid container item xs={12}>
            <Grid item xs={3}>
                <ItemAutocomplete
                    onSelectItem={(item) => setSelectedItem(item)}
                />
            </Grid>
            <Grid item xs={1}>
                {selectedItem && (
                    <IconButton aria-label='delete'>
                        <AddCircleOutlineIcon onClick={handleAddItem} />
                    </IconButton>
                )}
            </Grid>
            {values.items && (
                <Grid item xs={12} sx={{ mt: 1 }}>
                    {values.items.map((i) => (
                        <ItemAccordion item={i} />
                    ))}
                </Grid>
            )}
        </Grid>
    );
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
                            <CharacterAddedItemForm />
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
