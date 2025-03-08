import {
    Container,
    Grid,
    Grid2,
    LinearProgress,
    Pagination,
    Typography,
} from '@mui/material';
import { useEffect, useState } from 'react';
import RoomListItem from './RoomListItem';
import useUserInfoContext from '../hooks/useUserInfoContext';
import { getAsync } from '../axios';
import { API_GET_PAGED_ROOMS } from '../contants';
import { useNavigate } from 'react-router-dom';

const roomListBuilder = (roomList, onClick) =>
    roomList?.map((room) => (
        <Grid2 size={12} key={room.roomId}>
            <RoomListItem
                key={room.roomId}
                name={room.name}
                onClick={() => onClick(room)}
            />
        </Grid2>
    ));

const ROOM_TYPE = {
    Created: 0,
    Joined: 1,
};

const PAGE_SIZE = 9;
const RoomList = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [createdRooms, setCreatedRooms] = useState([]);
    const [joinedRooms, setJoinedRooms] = useState([]);
    const [page, setPage] = useState(1);
    const [pageCount, setPageCount] = useState(0);

    const userInfo = useUserInfoContext();
    const navigate = useNavigate();

    useEffect(() => {
        if (!userInfo) return;

        loadRoomList(ROOM_TYPE.Created, setCreatedRooms);
        loadRoomList(ROOM_TYPE.Joined, setJoinedRooms);
    }, [userInfo, page]);

    const loadRoomList = (roomType, roomSetter) => {
        (async () => {
            setIsLoading(true);
            const { isOk, data } = await getAsync(
                API_GET_PAGED_ROOMS +
                    `?pageSize=${PAGE_SIZE}&pageIndex=${
                        page - 1
                    }&roomType=${roomType}`
            );
            if (isOk) {
                roomSetter(data.value);
                setPageCount(data.totalPages);
            }
            setIsLoading(false);
        })();
    };

    const handleRoomClick = (room) => {
        navigate(`/rooms/${room.roomId}`);
    };

    return (
        <Container maxWidth='lg' sx={{ mt: 1 }}>
            <LinearProgress
                color='mimicLoader'
                sx={{ visibility: isLoading ? 'visible' : 'hidden', mb: 1 }}
            />
            <Grid2 container size={12} spacing={4}>
                <Grid2 size={6} container>
                    <Grid2 size={6}>
                        <Typography variant='h3'>My created rooms</Typography>
                    </Grid2>
                    {roomListBuilder(createdRooms, handleRoomClick)}
                </Grid2>
                <Grid2 size={6} container>
                    <Grid2 size={6}>
                        <Typography variant='h3'>My joined rooms</Typography>
                    </Grid2>
                    {roomListBuilder(joinedRooms, handleRoomClick)}
                </Grid2>
            </Grid2>
        </Container>
    );
};

export default RoomList;
