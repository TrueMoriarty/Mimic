import {
    Dialog,
    DialogContent,
    DialogContentText,
    DialogTitle,
} from '@mui/material';

const CharacterDialog = ({ itemId, ...props }) => {
    return (
        <Dialog {...props}>
            <DialogTitle>Item name</DialogTitle>
            <DialogContent>
                Let Google help apps determine location. This means sending
                anonymous location data to Google, even when no apps are
                running.
            </DialogContent>
        </Dialog>
    );
};

export default CharacterDialog;
