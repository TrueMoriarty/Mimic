import { useState } from 'react';
import CharacterList from './CharacterList';
import CharacterFormDailog from './CharacterFormDialog';
import { Button, Container, Stack } from '@mui/material';

const Characters = () => {
    const [isOpenCharaterForm, setOpenCharaterForm] = useState();

    return (
        <>
            <Container>
                <Stack direction='row' spacing={2}>
                    <Button color='mimicLoader'>Create</Button>
                </Stack>
            </Container>
            <CharacterList />
            <CharacterFormDailog
                open={isOpenCharaterForm}
                onClose={() => setOpenCharaterForm(false)}
            />
        </>
    );
};

export default Characters;
