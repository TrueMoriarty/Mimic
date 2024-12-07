import { Card, CardContent, Grid, Typography } from '@mui/material';
import ImageBox from '../Components/ImageBox';
import RoomNameTitle from './RoomName';
import { trunicateTypographyStyle } from '../mimicTheme';

const CharacterListItem = ({
    name,
    roomName,
    description,
    coverUrl,
    onClick,
}) => {
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
                    <Grid item md={12} lg={6}>
                        <ImageBox url={coverUrl} />
                    </Grid>
                    <Grid item md={12} lg={6}>
                        <Grid container spacing={1} direction='column'>
                            <Grid item xs={1}>
                                <RoomNameTitle roomName={roomName} />
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

export default CharacterListItem;
