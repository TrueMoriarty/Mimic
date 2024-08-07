import { TextField } from "@mui/material";
import { useField } from "formik";

const TextFieldFormik = (props) => {
    const [field, meta, helpers] = useField(props);

    const errorMessage = meta.touched && meta.error;

    return <TextField
        error={errorMessage}
        helperText={errorMessage}
        {...field}
        {...props}
    />
}

export default TextFieldFormik;