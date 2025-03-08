import { Button, Container, Stack } from '@mui/material';
import { useState } from 'react';
import RoomDialog from './RoomDialog';
import RoomList from './RoomList';
import JoiningRoomDialog from './JoiningRoomDialog';

const Rooms = () => {
    const [isOpenRoomForm, setIsOpenRoomForm] = useState(false);
    const [isJoinRoomForm, setIsJoinRoomForm] = useState(false);
    return (
        <>
            <Container sx={{ mt: 1 }}>
                <Stack direction='row' spacing={2}>
                    <Button
                        color='mimicSelected'
                        variant='contained'
                        onClick={() => setIsOpenRoomForm(true)}
                    >
                        Create
                    </Button>
                    <Button
                        color='mimicSelected'
                        variant='contained'
                        onClick={() => setIsJoinRoomForm(true)}
                    >
                        Join
                    </Button>
                </Stack>
            </Container>
            <RoomList />
            <RoomDialog
                open={isOpenRoomForm}
                onClose={() => setIsOpenRoomForm(false)}
            />
            <JoiningRoomDialog
                open={isJoinRoomForm}
                onClose={() => setIsJoinRoomForm(false)}
            />
        </>
    );
};

export default Rooms;
