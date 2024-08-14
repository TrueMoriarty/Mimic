import { Button, Card, CardHeader, Grid, Stack } from "@mui/material";
import { Formik } from "formik";
import * as Yup from 'yup';
import axios from "axios";
import { useNavigate } from "react-router-dom";
import TextFieldFormik from "../Components/Formik/TextFieldFormik";


const initUnbourdingValues = {
  username: null
};

const unbordingSchema = Yup.object().shape({
  username: Yup.string().min(3, 'Too Short!').required('Required')
});

const UnbordingForm = () => {
  const navigate = useNavigate();

  const handleSubmit = async (values) => {
    axios.defaults.withCredentials = true;
    try {
      const response = await axios.post("https://localhost/api/auth/unbord", { username: values.username });
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <Formik
      initialValues={initUnbourdingValues}
      onSubmit={handleSubmit}
      validationSchema={unbordingSchema}
    >
      {props => (
        <form onSubmit={props.handleSubmit} >
          <Stack direction="column" spacing={1}>
            <TextFieldFormik name="username" label="Username" variant="standard" />
            <Button variant="contained" type="submit">Unbord</Button>
          </Stack>
        </form>)
      }
    </Formik >
  );
}

export const Unbording = () => {
  return (
    <Grid container
      justifyContent="center"
      alignItems="center"
      sx={{ m: 2 }}
    >
      <Grid item xs={4}>
        <Card sx={{ p: 2 }}>
          <CardHeader title="Unbording" sx={{ p: 0 }} />
          <UnbordingForm />
        </Card>
      </Grid>
    </Grid>);
};
