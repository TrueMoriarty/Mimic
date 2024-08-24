import { Container, Grid, LinearProgress, Pagination } from '@mui/material';
import { useEffect, useState } from 'react';
import { getAsync } from '../axios';
import { GET_PAGED_ITEMS } from '../contants';
import ItemDialog from './ItemDialog';
import ItemListProperty from './ItemListProperty';
import useUserInfoContext from '../hooks/useUserInfoContext';

const PAGE_SIZE = 6;

const ItemList = () => {
    const [itemList, setItemList] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [isOpenDialog, setIsOpenDialog] = useState(false);
    const [selectedItemId, setSelectedItemId] = useState(null);
    const [page, setPage] = useState(1);
    const [pageCount, setPageCount] = useState(0);

    const userInfo = useUserInfoContext();

    useEffect(() => {
        if (!userInfo) return;

        (async () => {
            await loadItemList();
        })();
    }, [page, userInfo]);

    const loadItemList = async () => {
        setIsLoading(true);
        const { isOk, data } = await getAsync(
            GET_PAGED_ITEMS + `?pageSize=${PAGE_SIZE}&pageIndex=${page - 1}`
        );
        if (isOk) {
            setItemList(data.value);
            setPageCount(data.totalPages);
        }

        setIsLoading(false);
    };

    const handleSelectItem = (itemId) => {
        setIsOpenDialog(true);
        setSelectedItemId(itemId);
    };

    const handleCloseItemDialog = () => {
        setIsOpenDialog(false);
        setSelectedItemId(null);
    };

    return (
        <Container maxWidth='lg' sx={{ mt: 1 }}>
            {isLoading && <LinearProgress color='mimicLoader' />}
            <Grid container spacing={4} alignItems='center' sx={{ mt: 1 }}>
                {itemList?.map((item) => (
                    <Grid item xs={12} sm={4} key={item.itemId}>
                        <ItemListProperty
                            key={item.itemId}
                            name={item.name}
                            description={item.description}
                            onClick={() => handleSelectItem(item.itemId)}
                        />
                    </Grid>
                ))}
                {pageCount > 1 && (
                    <Grid item xs={12} container justifyContent={'center'}>
                        <Pagination
                            count={pageCount}
                            variant='outlined'
                            shape='rounded'
                            page={page}
                            onChange={(event, value) => setPage(value)}
                        />
                    </Grid>
                )}
            </Grid>
            <ItemDialog
                itemId={selectedItemId}
                open={isOpenDialog}
                onClose={handleCloseItemDialog}
            />
        </Container>
    );
};

export default ItemList;
