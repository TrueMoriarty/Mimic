import {
    CircularProgress,
    Dialog,
    DialogContent,
    DialogTitle,
    Grid,
    Typography,
} from '@mui/material';

import { useEffect, useState } from 'react';
import { getItemById } from '../contants';
import { getAsync } from '../axios';
import ImageBox from '../Components/ImageBox';
import PropertyAccordion from '../Property/PropertyAccordion';
import PropertyNameTitle from './PropertyNameTitle';
import ItemAccordion from '../Items/ItemAccordion';

const ItemDialog = ({ itemId, open, onClose }) => {
    const [item, setItem] = useState();
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        if (!itemId || !open) return;

        (async () => {
            setIsLoading(true);
            const { isOk, data } = await getAsync(getItemById(itemId));
            isOk && setItem(data);
            setIsLoading(false);
        })();
    }, [open]);

    const handleClose = () => {
        onClose?.();
        setItem(null);
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
            {item && (
                <>
                    <DialogTitle>
                        {item.name}{' '}
                        <PropertyNameTitle propertyName={item.propertyName} />
                    </DialogTitle>
                    <DialogContent>
                        <Grid container spacing={1}>
                            <Grid item xs={12} md={4}>
                                <ImageBox />
                            </Grid>
                            <Grid item xs={12} md={8}>
                                <Typography variant='body2'>
                                    {item.description}
                                </Typography>
                            </Grid>
                            {item.properties && (
                                <Grid property xs={12}>
                                    {item.properties.map((i) => (
                                        <ItemAccordion property={i} />
                                    ))}
                                </Grid>
                            )}
                        </Grid>
                    </DialogContent>
                </>
            )}
        </Dialog>
    );
};
export default ItemDialog;
