import {
    Accordion,
    AccordionDetails,
    AccordionSummary,
    Typography,
} from '@mui/material';
import { trunicateTypographyStyle } from '../mimicTheme';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { useState } from 'react';

const PropertyAccordion = ({ property }) => {
    const [expanded, setExpanded] = useState(false);

    return (
        <Accordion
            expanded={expanded}
            onChange={() => setExpanded((exp) => !exp)}
        >
            <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                <Typography sx={{ width: 'sm', flexShrink: 0, mr: 1 }}>
                    {property.name}
                </Typography>
            </AccordionSummary>
            <AccordionDetails>
                {expanded && (
                    <Typography sx={trunicateTypographyStyle}>
                        {property.description}
                    </Typography>
                )}
            </AccordionDetails>
        </Accordion>
    );
};

export default PropertyAccordion;
