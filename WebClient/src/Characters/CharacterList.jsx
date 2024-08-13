import { Container, Grid, LinearProgress, Pagination } from '@mui/material';
import AddCardButton from '../Components/AddCardButton';
import { useEffect, useState } from 'react';
import { getAsync } from '../axios';
import { GET_CREATOR_CHARACTERS } from '../contants';
import CharacterDialog from './CharacterDialog';
import CharacterListItem from './CharacterListItem';

const CharacterList = () => {
    const [characterList, setCharacterList] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [isOpenDialog, setIsOpenDialog] = useState(false);
    const [selectedCharacterId, setSelectedCharacterId] = useState(null);

    useEffect(() => {
        (async () => {
            setIsLoading(true);
            const { isOk, data } = await getAsync(GET_CREATOR_CHARACTERS);
            isOk && setCharacterList(data.characters);
            setIsLoading(false);
        })();
    }, []);

    const handleAdd = () => {};

    const handleSelectCharacter = (characterId) => {
        setIsOpenDialog(true);
        setSelectedCharacterId(characterId);
    };

    const handleCloseCharacterDialog = () => {
        setIsOpenDialog(false);
        setSelectedCharacterId(null);
    };

    return (
        <Container maxWidth='lg' sx={{ mt: 1 }}>
            {isLoading && <LinearProgress color='mimicLoader' sx={{ mb: 1 }} />}
            <Grid container spacing={4} alignItems='center'>
                {characterList?.map((character) => (
                    <Grid item xs={12} sm={4} key={character.characterId}>
                        <CharacterListItem
                            key={character.characterId}
                            name={character.name}
                            roomName={character.roomName}
                            description={character.description}
                            onClick={() =>
                                handleSelectCharacter(character.characterId)
                            }
                        />
                    </Grid>
                ))}
                <Grid item xs={12} sm={4}>
                    <AddCardButton onClick={handleAdd} />
                </Grid>
                <Grid item xs={12} container justifyContent={'center'}>
                    <Pagination count={10} variant='outlined' shape='rounded' />
                </Grid>
            </Grid>
            <CharacterDialog
                characterId={selectedCharacterId}
                open={isOpenDialog}
                onClose={handleCloseCharacterDialog}
            />
        </Container>
    );
};

export default CharacterList;
