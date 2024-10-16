import {
    Accordion,
    AccordionDetails,
    AccordionSummary,
    Button,
    ButtonGroup,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    Stack,
    Typography,
} from '@mui/material';
import { trunicateTypographyStyle } from '../mimicTheme';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import CircleIcon from '@mui/icons-material/Circle';
import { useState } from 'react';

const ItemAccordion = ({ item, onEdit, onDelete }) => {
    const [expanded, setExpanded] = useState(false);
    const properties = item.properties;

    const handleEditItem = (e) => {
        e.stopPropagation();
        onEdit?.(item);
    };

    const handleDeleteItem = (e) => {
        e.stopPropagation();
        onDelete?.(item);
    };

    return (
        <Accordion
            expanded={expanded}
            onChange={() => setExpanded((exp) => !exp)}
        >
            <AccordionSummary sx={{ width: 1 }} expandIcon={<ExpandMoreIcon />}>
                <Stack
                    direction='row'
                    spacing={1}
                    sx={{
                        justifyContent: 'space-between',
                        alignItems: 'center',
                        width: '100%',
                        mr: 1,
                    }}
                >
                    <Stack
                        direction='row'
                        spacing={1}
                        sx={{
                            justifyContent: 'space-between',
                            alignItems: 'center',
                        }}
                    >
                        <Typography>{item.name}</Typography>
                        {!expanded && (
                            <Typography
                                sx={{
                                    ...trunicateTypographyStyle,
                                }}
                            >
                                {item.description}
                            </Typography>
                        )}
                    </Stack>
                    <ButtonGroup size='small' color='mimicSelected'>
                        <Button onClick={handleEditItem}>Edit</Button>
                        <Button onClick={handleDeleteItem}>Delete</Button>
                    </ButtonGroup>
                </Stack>
            </AccordionSummary>
            <AccordionDetails>
                {expanded && (
                    <Typography sx={{ ...trunicateTypographyStyle, mb: 1 }}>
                        {item.description}
                    </Typography>
                )}
                {properties && (
                    <List sx={{ py: 0 }}>
                        {properties.map((p) => (
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

export default ItemAccordion;
