import {
    Accordion,
    AccordionDetails,
    AccordionSummary,
    CircularProgress,
    Dialog,
    DialogContent,
    DialogTitle,
    Grid,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    Typography,
} from '@mui/material';
import ImageBox from '../Components/ImageBox';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import CircleIcon from '@mui/icons-material/Circle';
import { useEffect, useState } from 'react';
import RoomNameTitle from './RoomName';
import { trunicateTypographyStyle } from '../mimicTheme';
import { getCharacterById } from '../contants';
import { getAsync } from '../axios';

const ItemAccordion = ({ item }) => {
    const [expanded, setExpanded] = useState(false);
    const properies = item.properies;
    return (
        <Accordion
            expanded={expanded}
            onChange={() => setExpanded((exp) => !exp)}
        >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                <Typography sx={{ width: 'sm', flexShrink: 0, mr: 1 }}>
                    {item.name}
                </Typography>
                {!expanded && (
                    <Typography sx={trunicateTypographyStyle}>
                        {item.description}
                    </Typography>
                )}
            </AccordionSummary>
            <AccordionDetails>
                {expanded && (
                    <Typography sx={{ ...trunicateTypographyStyle, mb: 1 }}>
                        {item.description}
                    </Typography>
                )}
                {properies && (
                    <List sx={{ py: 0 }}>
                        {properies.map((p) => (
                            <ListItem disableGutters>
                                <ListItemIcon sx={{ justifyContent: 'center' }}>
                                    <CircleIcon sx={{ fontSize: 11 }} />
                                </ListItemIcon>
                                <ListItemText
                                    primary={`${p.name} - ${p.description}`}
                                />
                            </ListItem>
                        ))}
                    </List>
                )}
            </AccordionDetails>
        </Accordion>
    );
};

const CharacterDialog = ({ characterId, open, onClose }) => {
    const [character, setCharacter] = useState();
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        if (!characterId || !open) return;

        (async () => {
            setIsLoading(true);
            const { isOk, data } = await getAsync(
                getCharacterById(characterId)
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
                    <DialogTitle>{character.name}</DialogTitle>
                    <DialogContent>
                        <Grid container spacing={1}>
                            <Grid item xs={12} md={4}>
                                <ImageBox />
                            </Grid>
                            <Grid item xs={12} md={8}>
                                <RoomNameTitle roomName={character.roomName} />
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
