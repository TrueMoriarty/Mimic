import { Grid, LinearProgress, } from "@mui/material";

import CharacterCard from "./CharacterCard";
import AddCardButton from "../Components/AddCardButton";
import { useState } from "react";

const items = [
    { name: "Alisa", status: "In game COOOLER", description: "Lorem ipsum dolor sit amet, consectetur " },
];


const Characters = () => {
    const [characterList, setCharacterList] = useState(items);

    const handleAdd = () => {
        setCharacterList([...characterList, { name: "Test ch", status: "In game", description: "Aloha" }])
    }

    return (
        <>
            <Grid container spacing={4} sx={{ mt: 1 }}>
                {characterList.map(item =>
                    <Grid item xs={4}>
                        <CharacterCard name={item.name} status={item.status} description={item.description} />
                    </Grid>)
                }
                <Grid item xs={4}><AddCardButton onClick={handleAdd} /></Grid>
            </Grid >
        </>
    )
};

export default Characters;
