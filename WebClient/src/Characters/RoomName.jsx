import { Typography } from '@mui/material';

const RoomNameTitle = ({ roomName }) => {
    const name = roomName ? `В игре "${roomName}"` : 'Шаблон';
    return <Typography variant='caption'>{name}</Typography>;
};

export default RoomNameTitle;
