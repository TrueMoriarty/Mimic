import { Card, CardContent, Grid, Typography } from '@mui/material';
import ImageBox from '../Components/ImageBox';
import PropertyNameTitle from './PropertyNameTitle';
import { trunicateTypographyStyle } from '../mimicTheme';

const ItemListProperty = ({ name, propertyName, description, onClick }) => {
    return (
        <Card
            onClick={onClick}
            sx={{
                ':hover': {
                    boxShadow: 10,
                },
            }}
        >
            <CardContent>
                <Grid container spacing={1}>
                    <Grid item xs={12} md={6}>
                        <ImageBox />
                    </Grid>
                    <Grid item xs={'auto'}>
                        <Grid container spacing={1} direction='column'>
                            <Grid item xs={1}>
                                <PropertyNameTitle
                                    propertyName={propertyName}
                                />
                            </Grid>
                            <Grid item xs={1}>
                                <Typography variant='h6'>{name}</Typography>
                            </Grid>
                            <Grid item xs={10}>
                                <Typography
                                    variant='body2'
                                    sx={trunicateTypographyStyle}
                                >
                                    {description}
                                </Typography>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    );
};

export default ItemListProperty;
