import {
    Box,
    Button,
    Card,
    Divider,
    Grid,
    Stack,
    TextField,
    Typography,
} from '@mui/material';
import { Form, Formik } from 'formik';

import { postAsync } from '../axios';
import TextFieldFormik from '../Components/Formik/TextFieldFormik';
import useNotification from '../hooks/useNotification';

const Login = () => {
    const { notifyError } = useNotification();

    const handleAuth = async () =>
        window.location.assign('https://localhost/api/auth/oidc/vk');

    const handleSubmit = async (values) => {
        if (!values.id) {
            notifyError('empty id');
            return;
        }
        const { isOk, data } = await postAsync(
            `https://localhost/api/test/testLogin?id=${values.id}`
        );
    };

    return (
        <Grid
            container
            justifyContent='center'
            alignItems='center'
            sx={{ m: 2 }}
        >
            <Grid item xs={4}>
                <Card>
                    <Box sx={{ p: 1 }}>
                        <Stack direction='column' spacing={1}>
                            <Typography variant='h6'>Log in with:</Typography>
                            <Button variant='contained' onClick={handleAuth}>
                                Vk
                            </Button>
                        </Stack>
                    </Box>
                    <Divider />
                    <Box sx={{ p: 1 }}>
                        <Formik
                            onSubmit={handleSubmit}
                            initialValues={{ id: null }}
                        >
                            <Form>
                                <Stack direction='column' spacing={1}>
                                    <TextFieldFormik
                                        name='id'
                                        label='Id'
                                        variant='standard'
                                        size='small'
                                    />
                                    <Button type='submit' variant='outlined'>
                                        Login
                                    </Button>
                                </Stack>
                            </Form>
                        </Formik>
                    </Box>
                    <Box sx={{ p: 1 }}>
                        <Stack direction='column' spacing={1}>
                            <TextField
                                label='Login'
                                variant='standard'
                                disabled
                            />
                            <TextField
                                label='Password'
                                variant='standard'
                                type='password'
                                disabled
                                autoComplete='current-password'
                            />
                            <Button variant='contained' disabled>
                                Login
                            </Button>
                            <Button variant='contained' disabled>
                                Registration
                            </Button>
                        </Stack>
                    </Box>
                </Card>
            </Grid>
        </Grid>
    );
};

export default Login;
