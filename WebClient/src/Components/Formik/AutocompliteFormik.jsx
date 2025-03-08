import { Autocomplete } from '@mui/material';
import { useField } from 'formik';

const AutocompliteFormik = (props) => {
    const [field, meta, helpers] = useField(props);
    return <Autocomplete {...field} {...props} />;
};

export default AutocompliteFormik;
