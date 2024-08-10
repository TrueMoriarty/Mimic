import { Box, Card, CardContent, Grid, Typography } from "@mui/material";
import PersonIcon from '@mui/icons-material/Person';

const CharacterCard = ({ name, status, description }) => {
    return (
        <Card>
            <CardContent>
                <Grid container spacing={1}>
                    <Grid item xs={6}>
                        <Box
                            height={150}
                            width={150}
                            display="flex"
                            alignItems={"center"}
                            justifyContent={"center"}
                            sx={{
                                borderRadius: 1,
                                bgcolor: 'mimic.main',
                            }}>
                            <PersonIcon sx={{ fontSize: 40 }} />
                        </Box>
                    </Grid>
                    <Grid item xs={6}>
                        <Grid
                            container
                            spacing={1}
                            direction="column"
                        >
                            <Grid item xs={1}><Typography variant="caption">{status}</Typography></Grid>
                            <Grid item xs={1}><Typography variant="h6">{name}</Typography></Grid>
                            <Grid item xs={10} >
                                <Typography
                                    variant="body2"
                                    sx={{
                                        display: '-webkit-box',
                                        overflow: 'hidden',
                                        WebkitBoxOrient: 'vertical',
                                        WebkitLineClamp: 4,
                                    }}>{description}</Typography>

                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
            </CardContent>
        </Card >
    );
};

export default CharacterCard;
