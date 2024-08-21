import {
    Accordion,
    AccordionDetails,
    AccordionSummary,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    Typography,
} from '@mui/material';
import { trunicateTypographyStyle } from '../mimicTheme';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import CircleIcon from '@mui/icons-material/Circle';
import { useState } from 'react';

const PropertyAccordion = ({ property }) => {
    const [expanded, setExpanded] = useState(false);
    const properties = property;

    return (
        <Accordion
            expanded={expanded}
            onChange={() => setExpanded((exp) => !exp)}
        >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                <Typography sx={{ width: 'sm', flexShrink: 0, mr: 1 }}>
                    {property.name}
                </Typography>
                {!expanded && (
                    <Typography sx={trunicateTypographyStyle}>
                        {property.description}
                    </Typography>
                )}
            </AccordionSummary>
            <AccordionDetails>
                {expanded && (
                    <Typography sx={{ ...trunicateTypographyStyle, mb: 1 }}>
                        {property.description}
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

export default PropertyAccordion;
