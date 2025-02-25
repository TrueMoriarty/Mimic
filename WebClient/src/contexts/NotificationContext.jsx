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

    const notify = (message, type) => {
        setOpen(true);
        setNotifies([...notifies, { message, type }]);
    };

    const notifySuccess = (message) => notify(message, NotifyType.Success);

    const notifyError = (message) => notify(message, NotifyType.Error);

    const notifyWarning = (message) => notify(message, NotifyType.Warning);

    const removeNotification = (index) => {
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
                notify: notify,
                notifySuccess: notifySuccess,
                notifyError: notifyError,
                notifyWarning: notifyWarning,
                clearAllNotification: clearAllNotification,
            }}
        >
            {children}
            {notifies && (
                <Stack
                    spacing={1}
                    sx={{
                        zIndex: 'tooltip',
                        position: 'absolute',
                        bottom: '5%',
                        left: '70%',
                        width: '25%',
                    }}
                >
                    {notifies.map((notify, index) => (
                        <Zoom in={open}>
                            <Alert
                                severity={notify.type}
                                action={
                                    <IconButton
                                        aria-label='close'
                                        color='inherit'
                                        size='small'
                                        onClick={() => {
                                            removeNotification(index);
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
