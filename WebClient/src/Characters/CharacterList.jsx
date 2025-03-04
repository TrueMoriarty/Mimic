import { Container, Grid, LinearProgress, Pagination } from '@mui/material';
import { useEffect, useState } from 'react';
import { getAsync } from '../axios';
import { API_GET_PAGED_CHARACTERS, DAILOG_MODE } from '../contants';
import CharacterListItem from './CharacterListItem';
import useUserInfoContext from '../hooks/useUserInfoContext';
import CharacterDailog from './CharacterDialog';

const PAGE_SIZE = 9;

const CharacterList = () => {
    const [characterList, setCharacterList] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [isOpenDialog, setIsOpenDialog] = useState(false);
    const [selectedCharacterId, setSelectedCharacterId] = useState(null);
    const [page, setPage] = useState(1);
    const [pageCount, setPageCount] = useState(0);

    const userInfo = useUserInfoContext();

    useEffect(() => {
        if (!userInfo) return;

        loadCharacterList();
    }, [page, userInfo]);

    const loadCharacterList = () => {
        setIsLoading(true);
        (async () => {
            const { isOk, data } = await getAsync(
                API_GET_PAGED_CHARACTERS +
                    `?pageSize=${PAGE_SIZE}&pageIndex=${page - 1}&creatorId=${
                        userInfo.userId
                    }`
            );
            if (isOk) {
                setCharacterList(data.value);
                setPageCount(data.totalPages);
            }
        })();

        setIsLoading(false);
    };

    const handleSelectCharacter = (characterId) => {
        setIsOpenDialog(true);
        setSelectedCharacterId(characterId);
    };

    const handleCloseCharacterDialog = () => {
        loadCharacterList();

        setIsOpenDialog(false);
        setSelectedCharacterId(null);
    };

    return (
        <Container maxWidth='lg' sx={{ mt: 1 }}>
            <LinearProgress
                color='mimicLoader'
                sx={{ visibility: isLoading ? 'visible' : 'hidden', mb: 1 }}
            />
            <Grid container spacing={4} alignItems='center'>
                {characterList?.map((character) => (
                    <Grid item xs={12} sm={4} key={character.characterId}>
                        <CharacterListItem
                            key={character.characterId}
                            name={character.name}
                            roomName={character.roomName}
                            description={character.description}
                            coverUrl={character.coverUrl}
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
            <CharacterDailog
                characterId={selectedCharacterId}
                open={isOpenDialog}
                onClose={handleCloseCharacterDialog}
                dialogMode={DAILOG_MODE.EDIT}
            />
        </Container>
    );
};

export default CharacterList;
