import { createTheme } from '@mui/material';

//todo: поправить цветовую схему. А то тут костыль какой-то
export const mimicTheme = createTheme({
    palette: {
        //custom colors
        mimic: {
            main: '#DFC7A7',
            light: '#EFDCC2',
            dark: '#916A36',
            contrastText: '#4b3a23',
        },
        mimicSelected: {
            main: '#6E8190',
            light: '#A4B8C7',
            dark: '#24445D',
            contrastText: '#AEBCC7',
        },
        mimicLoader: {
            main: '#E4EBBF',
            light: '#A4B8C7',
        },

        // override colors
        background: {
            paper: '#EFDCC2',
        },
    },
});

export const trunicateTypographyStyle = {
    color: 'text.secondary',
    display: '-webkit-box',
    overflow: 'hidden',
    WebkitBoxOrient: 'vertical',
    WebkitLineClamp: 3,
};
