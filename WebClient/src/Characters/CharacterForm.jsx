import { Grid, Stack } from '@mui/material';
import { Form, useFormikContext } from 'formik';
import ImageBox from '../Components/ImageBox';
import TextFieldFormik from '../Components/Formik/TextFieldFormik';
import ItemAccordion from '../Items/ItemAccordion';
import ItemSearchGroup from '../Items/ItemSearchGroup';
import { deleteAsync, postAsync } from '../axios';
import { API_ITEMS, getItemByIdUrl } from '../contants';
import useNotification from '../hooks/useNotification';

const CharacterForm = ({ readOnly }) => {
    const { values, setFieldValue } = useFormikContext();
    const items = values.storage?.items ?? [];

    const { notifySuccess, notifyWarning, notifyError } = useNotification();

    const handleAddItem = async (item) => {
        if (values.characterId) {
            const model = { ...item };
            model.storageId = values.storage.storageId;
            const { isOk, data } = await postAsync(API_ITEMS, model);
            if (isOk) {
                item.itemId = data;
                const newItems = [item, ...items];
                updateItemInStorage(newItems);
                notifySuccess('Предмет успешно добавлен');
            } else notifyError('Не удалось добавить предмет');
        } else {
            const newItems = [item, ...items];
            updateItemInStorage(newItems);
        }
    };

    const handleDeleteItem = async (item) => {
        const deletingItemId = item.itemId;
        const { isOk } = await deleteAsync(getItemByIdUrl(deletingItemId));
        if (isOk) {
            const newItems = [...items];
            const index = items.findIndex((i) => i.itemId === deletingItemId);
            newItems.splice(index, 1);
            updateItemInStorage(newItems);
            notifySuccess('Предмет успешно удален');
        } else {
            notifyError('Не удалось удалить предмет');
        }
    };

    const handleEditItem = (item) => {
        notifyWarning('Упс....Изменения предметов еще нет((');
    };

    const updateItemInStorage = (newItems) => {
        const newStorage = { ...values.storage, items: newItems };
        setFieldValue('storage', newStorage);
    };

    return (
        <Form>
            <Grid container spacing={1}>
                <Grid item xs={12} md={4}>
                    <ImageBox url={values.coverUrl} />
                </Grid>
                <Grid item xs={12} md={8}>
                    <Stack direction='column' spacing={1}>
                        <TextFieldFormik
                            name='name'
                            label='Name'
                            variant='standard'
                            InputProps={{
                                readOnly: readOnly,
                            }}
                            size='small'
                            color='mimicSelected'
                        />
                        <TextFieldFormik
                            name='description'
                            label='Description'
                            variant='standard'
                            size='small'
                            color='mimicSelected'
                            InputProps={{
                                readOnly: readOnly,
                            }}
                            multiline
                            maxRows={3}
                        />
                    </Stack>
                </Grid>
                {!readOnly && <ItemSearchGroup onAddItem={handleAddItem} />}
                {items && (
                    <Grid item xs={12} sx={{ mt: 1 }}>
                        {items.map((i, index) => (
                            <ItemAccordion
                                key={index}
                                item={i}
                                onDelete={handleDeleteItem}
                                onEdit={handleEditItem}
                            />
                        ))}
                    </Grid>
                )}
            </Grid>
        </Form>
    );
};

export default CharacterForm;
