import { Grid, LinearProgress, } from "@mui/material";

import CharacterCard from "./CharacterCard";

const Characters = () => {
    return (
        <>
            {/* <LinearProgress color="mimicLoader" sx={{ m: 1 }} /> */}
            <Grid container spacing={8}>
                <Grid item xs={4}>
                    <CharacterCard name={"Сисава"} status={"В игре"} description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis eget rhoncus lectus. Donec consequat tortor vitae lectus rhoncus, vel tincidunt urna consequat. Nulla et tortor faucibus, egestas libero malesuada, pharetra orci."} />
                </Grid>
                <Grid item xs={4}>
                    <CharacterCard />
                </Grid>
                <Grid item xs={4}>
                    <CharacterCard />
                </Grid>
            </Grid >
        </>
    )
};

export default Characters;
