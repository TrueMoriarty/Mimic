import { Button, Card, CardContent, LinearProgress } from "@mui/material";


const Characters = () => {
    return (
        <>
            <LinearProgress color="mimicLoader" sx={{ m: 1 }} />
            <Card sx={{ m: 1 }}>
                <CardContent>Test<Button>TEST</Button></CardContent>
            </Card>

        </>
    )
};

export default Characters;
