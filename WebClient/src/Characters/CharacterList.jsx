import { Container, Grid, LinearProgress, } from "@mui/material";
import CharacterCard from "./CharacterCard";
import AddCardButton from "../Components/AddCardButton";
import { useEffect, useState } from "react";
import { getAsync } from "../axios";
import { GET_CREATOR_CHARACTERS } from "../contants";


const CharacterList = () => {
    const [characterList, setCharacterList] = useState();
    const [isLoading, setIsLoading] = useState(false);

    const handleAdd = () => {
        setCharacterList([...characterList, { name: "Test ch", status: "In game", description: "Aloha" }])
    }

    useEffect(() => {
        (async () => {
            setIsLoading(true)
            const { isOk, data } = await getAsync(GET_CREATOR_CHARACTERS);
            isOk && setCharacterList(data.characters);
            setIsLoading(false)
        })()

    }, [])

    return (
        <Container sx={{ m: 1 }}>
            {isLoading && (<LinearProgress color="mimicLoader" sx={{ mb: 1 }} />)}
            <Grid container spacing={4}>
                {characterList?.map(item =>
                    <Grid item xs={12} sm={4} key={item.name.characterId}>
                        <CharacterCard name={item.name} roomName={item.roomName} description={item.description} />
                    </Grid>)
                }
                <Grid item xs={12} sm={4}><AddCardButton onClick={handleAdd} /></Grid>
            </Grid >
        </Container>
    )
};

export default CharacterList;
