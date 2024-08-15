import { useState } from 'react';
import CharacterList from './CharacterList';
import CharacterFormDailog from './CharacterFormDialog';
import { Button, Container, Stack } from '@mui/material';

const Characters = () => {
    const [isOpenCharaterForm, setOpenCharaterForm] = useState(false);

    return (
        <>
            <Container sx={{ mt: 1 }}>
                <Stack direction='row' spacing={2}>
                    <Button
                        color='mimicSelected'
                        variant='contained'
                        onClick={() => setOpenCharaterForm(true)}
                    >
                        Create
                    </Button>
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
