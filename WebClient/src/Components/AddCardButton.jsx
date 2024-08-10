import { Button, } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';

const AddCardButton = ({ onClick }) => {
    return (
        <Button
            onClick={onClick}
            color={"mimicSelected"}
            size="large"
            variant="outlined"
            sx={{ border: 5, width: "100%", maxHeight: "100%", minHeight: 190 }}>
            <AddIcon sx={{ fontSize: 100 }} />
        </Button >
    );
};

export default AddCardButton;
