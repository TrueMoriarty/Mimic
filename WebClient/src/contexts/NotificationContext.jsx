import { Alert, IconButton, Stack, Zoom } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';
import { createContext, useState } from 'react';

export const NotificationContext = createContext({});

export const NotifyType = {
    Success: 'success',
    Info: 'info',
    Warning: 'warning',
    Error: 'error',
};

function NotificationContextProvider({ children }) {
    const [open, setOpen] = useState(false);
    const [notifies, setNotifies] = useState([]);

    const pushNotify = (message, type) => {
        setOpen(true);
        setNotifies([...notifies, { message, type }]);
    };

    const pushSuccessNotify = (message) =>
        pushNotify(message, NotifyType.Success);

    const pushErrorNotify = (message) => pushNotify(message, NotifyType.Error);

    const removeNotify = (index) => {
        const deletedNotifies = [...notifies];
        deletedNotifies.splice(index, 1);
        setNotifies(deletedNotifies);
    };

    const clearAllNotification = () => {
        setOpen(false);
        setNotifies([]);
    };

    return (
        <NotificationContext.Provider
            value={{
                pushNotify: pushNotify,
                pushSuccessNotify: pushSuccessNotify,
                pushErrorNotify: pushErrorNotify,
                clearAllNotification: clearAllNotification,
            }}
        >
            {children}
            {notifies && (
                <Stack
                    spacing={1}
                    sx={{
                        zIndex: 'tooltip',
                        position: 'sticky',
                        bottom: 20,
                        left: '75%',
                        width: '25%',
                    }}
                >
                    {notifies.map((notify, index) => (
                        <Zoom in={open}>
                            <Alert
                                severity={notify.type}
                                icon={false}
                                action={
                                    <IconButton
                                        aria-label='close'
                                        color='inherit'
                                        size='small'
                                        onClick={() => {
                                            removeNotify(index);
                                        }}
                                    >
                                        <CloseIcon fontSize='inherit' />
                                    </IconButton>
                                }
                            >
                                {notify.message}
                            </Alert>
                        </Zoom>
                    ))}
                </Stack>
            )}
        </NotificationContext.Provider>
    );
}

export default NotificationContextProvider;
