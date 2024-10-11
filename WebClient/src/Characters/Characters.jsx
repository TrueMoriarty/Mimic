import { useState } from 'react';
import CharacterList from './CharacterList';
import CharacterDailog from './CharacterDialog';
import { Button, Container, Stack } from '@mui/material';
import { DAILOG_MODE } from '../contants';

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
            <CharacterDailog
                title={'Create character'}
                open={isOpenCharaterForm}
                onClose={() => setOpenCharaterForm(false)}
                dialogMode={DAILOG_MODE.CREATE}
            />
        </>
    );
};

export default Characters;
