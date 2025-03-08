import { Card, CardContent, Grid, Typography } from '@mui/material';

const RoomListItem = ({ name, onClick }) => {
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
                    <Typography variant='h6'>{name}</Typography>
                </Grid>
            </CardContent>
        </Card>
    );
};

export default RoomListItem;
