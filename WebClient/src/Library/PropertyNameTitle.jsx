import { Typography } from '@mui/material';

const PropertyNameTitle = ({ propertyName }) => {
    const name = propertyName ? `Свойство "${propertyName}"` : `Шаблон`;
    return <Typography variant='caption'>{name}</Typography>;
};

export default PropertyNameTitle;
