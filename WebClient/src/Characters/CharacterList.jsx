import { Container, Grid, LinearProgress, Pagination } from '@mui/material';
import AddCardButton from '../Components/AddCardButton';
import { useEffect, useState } from 'react';
import { getAsync } from '../axios';
import { GET_CREATOR_CHARACTERS } from '../contants';
import CharacterDialog from './CharacterDialog';
import CharacterListItem from './CharacterListItem';

const PAGE_SIZE = 6;

const CharacterList = () => {
    const [characterList, setCharacterList] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [isOpenDialog, setIsOpenDialog] = useState(false);
    const [selectedCharacterId, setSelectedCharacterId] = useState(null);
    const [page, setPage] = useState(1);
    const [pageCount, setPageCount] = useState(0);

    useEffect(() => {
        (async () => {
            await loadCharacterList();
        })();
    }, [page]);

    const loadCharacterList = async () => {
        setIsLoading(true);
        const { isOk, data } = await getAsync(
            GET_CREATOR_CHARACTERS +
                `?pageSize=${PAGE_SIZE}&pageIndex=${page - 1}`
        );
        if (isOk) {
            setCharacterList(data.value);
            setPageCount(data.totalPages);
        }

        setIsLoading(false);
    };

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
            {isLoading && <LinearProgress color='mimicLoader' />}
            <Grid container spacing={4} alignItems='center' sx={{ mt: 1 }}>
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
                {pageCount > 1 && (
                    <Grid item xs={12} container justifyContent={'center'}>
                        <Pagination
                            count={pageCount}
                            variant='outlined'
                            shape='rounded'
                            page={page}
                            onChange={(event, value) => setPage(value)}
                        />
                    </Grid>
                )}
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
