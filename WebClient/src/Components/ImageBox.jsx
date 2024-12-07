import { Box } from '@mui/material';
import PersonIcon from '@mui/icons-material/Person';

const ImageBox = ({ url }) => {
    if (url)
        return (
            <Box
                component={'img'}
                src={url}
                height={150}
                width={150}
                sx={{
                    borderRadius: 1,
                    bgcolor: 'mimic.main',
                }}
            />
        );

    return (
        <Box
            height={150}
            width={150}
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
