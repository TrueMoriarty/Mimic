import { Box, Dialog } from '@mui/material';
import PersonIcon from '@mui/icons-material/Person';
import { useState } from 'react';

const FullSizeImageBoxDialog = ({ open, url, onClose }) => {
    return (
        <Dialog fullWidth maxWidth={'lg'} open={open} onClose={onClose}>
            <Box
                component={'img'}
                src={url}
                sx={{
                    borderRadius: 1,
                    backgroundImage: `url(${url})`,
                    objectFit: 'cover',
                }}
            />
        </Dialog>
    );
};

const ImageBox = ({ url, width, height, hasFullResolutionShowing }) => {
    const [isShowFullImage, setShowFullImage] = useState(false);
    if (url)
        return (
            <>
                <Box
                    component={'img'}
                    src={url}
                    sx={{
                        borderRadius: 1,
                        width: width,
                        height: height,
                        backgroundImage: `url(${url})`,
                        objectFit: 'cover',
                    }}
                    onClick={() =>
                        hasFullResolutionShowing && setShowFullImage(true)
                    }
                />
                {hasFullResolutionShowing && (
                    <FullSizeImageBoxDialog
                        url={url}
                        open={isShowFullImage}
                        onClose={() => setShowFullImage(false)}
                    />
                )}
            </>
        );

    return (
        <Box
            height={width}
            width={height}
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
