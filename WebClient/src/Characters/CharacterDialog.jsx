import {
    CircularProgress,
    Dialog,
    DialogContent,
    DialogTitle,
    Grid,
    Typography,
} from '@mui/material';
import ImageBox from '../Components/ImageBox';
import { useEffect, useState } from 'react';
import RoomNameTitle from './RoomName';
import { getCharacterByIdURL } from '../contants';
import { getAsync } from '../axios';
import ItemAccordion from '../Items/ItemAccordion';

const CharacterDialog = ({ characterId, open, onClose }) => {
    const [character, setCharacter] = useState();
    const [isLoading, setIsLoading] = useState(true);

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
        setCharacter(null);
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
            {character && (
                <>
                    <DialogTitle>
                        {character.name}{' '}
                        <RoomNameTitle roomName={character.roomName} />
                    </DialogTitle>
                    <DialogContent>
                        <Grid container spacing={1}>
                            <Grid item xs={12} md={4}>
                                <ImageBox />
                            </Grid>
                            <Grid item xs={12} md={8}>
                                <Typography variant='body2'>
                                    {character.description}
                                </Typography>
                            </Grid>
                            {character.items && (
                                <Grid item xs={12}>
                                    {character.items.map((i) => (
                                        <ItemAccordion item={i} />
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
export default CharacterDialog;
