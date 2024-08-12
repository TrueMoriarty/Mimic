import { Button, } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';


const AddCardButtonStyles = {
    border: 5, width: "100%", maxHeight: "100%", minHeight: 190, ':hover': {
        boxShadow: 10,
        border: 5,
    },
};

const AddCardButton = ({ onClick }) => {
    return (
        <Button
            onClick={onClick}
            color={"mimicSelected"}
            size="large"
            variant="outlined"
            sx={AddCardButtonStyles}>
            <AddIcon sx={{ fontSize: 100 }} />
        </Button >
    );
};

export default AddCardButton;
