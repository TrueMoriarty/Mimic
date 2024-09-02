import { Grid, IconButton } from '@mui/material';
import ItemAutocomplete from './ItemAutocomplete';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import { useState } from 'react';
import { getAsync } from '../axios';
import { getItemByIdURL } from '../contants';

const ItemSearchGroup = ({ onAddItem }) => {
    const [selectedItem, setSelectedItem] = useState(null);

    const handleAddItem = async () => {
        const { isOk, data } = await getAsync(
            getItemByIdURL(selectedItem.itemId)
        );
        isOk && onAddItem?.(data);
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
        </Grid>
    );
};

export default ItemSearchGroup;
