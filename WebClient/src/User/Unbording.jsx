import { Button, Card, CardHeader, Grid, Stack, TextField } from "@mui/material";
import TextFieldFormik from "../Components/TextFieldFormik";
import { Formik } from "formik";
import * as Yup from 'yup';

const initUnbourdingValues = {
  nickname: null
};

const unbordingSchema = Yup.object().shape({
  nickname: Yup.string().min(2, 'Too Short!').required('Required')
});

const UnbordingForm = () => {

  const handleSubmit = (values) => {
    console.log(values)
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
            <TextFieldFormik name="nickname" label="Nickname" variant="standard" required />
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
