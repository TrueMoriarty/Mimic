import { Box, Button, Card, Divider, Grid, Stack, TextField, Typography } from "@mui/material";

const Login = () => {
  const handleAuth = async () =>
    window.location.assign("https://localhost/api/auth/oidc/vk");

  return (
    <Grid container
      justifyContent="center"
      alignItems="center"
      sx={{ m: 2 }}
    >
      <Grid item xs={4}>
        <Card>
          <Box sx={{ p: 1 }}>
            <Stack direction="column" spacing={1}>
              <Typography variant="h6">Log in with:</Typography>
              <Button variant="contained" onClick={handleAuth}>Vk</Button>
            </Stack>
          </Box>
          <Divider />
          <Box sx={{ p: 1 }}>
            <Stack direction="column" spacing={1}>
              <TextField label="Login" variant="standard" disabled />
              <TextField label="Password" variant="standard" type="password" disabled autoComplete="current-password" />
              <Button variant="contained" disabled>Login</Button>
              <Button variant="contained" disabled>Registration</Button>
            </Stack>
          </Box>
        </Card>
      </Grid>
    </Grid>
  );
};

export default Login;
