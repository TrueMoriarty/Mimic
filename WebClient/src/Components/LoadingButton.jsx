import { Button, CircularProgress } from '@mui/material';

const LoadingButton = ({ caption, isLoading, ...props }) => {
    return (
        <Button {...props}>
            {caption}
            {isLoading && (
                <CircularProgress
                    color={'mimicLoader'}
                    size={20}
                    sx={{ ml: 1 }}
                />
            )}
        </Button>
    );
};

export default LoadingButton;
