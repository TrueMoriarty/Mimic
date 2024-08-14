import { Box } from '@mui/material';
import PersonIcon from '@mui/icons-material/Person';

const ImageBox = () => {
    return (
        <Box
            height={150}
            width={'sm'}
            display='flex'
            alignItems={'center'}
            justifyContent={'center'}
            sx={{
                borderRadius: 1,
                bgcolor: 'mimic.main',
            }}
        >
            <PersonIcon sx={{ fontSize: 40 }} />
        </Box>
    );
};

export default ImageBox;
