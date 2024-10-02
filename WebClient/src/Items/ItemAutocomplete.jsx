import { Autocomplete, Stack, TextField, Typography } from '@mui/material';
import { useState } from 'react';
import { getAsync } from '../axios';
import { getItemSuggestsURL } from '../contants';
import parse from 'autosuggest-highlight/parse';
import match from 'autosuggest-highlight/match';

const ItemAutocomplete = ({ onSelectItem }) => {
    const [value, setValue] = useState(null);
    const [inputValue, setInputValue] = useState('');
    const [options, setOptions] = useState([]);

    const handleSearch = async (query) => {
        if (!query || query.length < 3) return;

        const { isOk, data } = await getAsync(getItemSuggestsURL(query, 45));

        if (isOk) setOptions(data);
    };

    return (
        <Autocomplete
            freeSolo
            value={value}
            size='small'
            onChange={(event, newValue) => {
                setValue(newValue);
                onSelectItem?.(newValue);
            }}
            getOptionLabel={(option) => option.name}
            isOptionEqualToValue={(option, value) => option.id === value.id}
            inputValue={inputValue}
            options={options}
            onInputChange={(event, newInputValue) => {
                handleSearch(newInputValue);
                setInputValue(newInputValue);
            }}
            renderInput={(params) => (
                <TextField
                    {...params}
                    color={'mimicSelected'}
                    label='Search add items'
                />
            )}
            renderOption={(props, option) => {
                const { key, ...optionProps } = props;

                const title = option.name;
                const matches = match(title, inputValue, {
                    insideWords: true,
                });
                const parts = parse(title, matches);

                return (
                    <li key={key} {...optionProps}>
                        <Stack>
                            <Typography>
                                {parts.map((part, index) => (
                                    <span
                                        key={index}
                                        style={{
                                            fontWeight: part.highlight
                                                ? 700
                                                : 400,
                                        }}
                                    >
                                        {part.text}
                                    </span>
                                ))}
                            </Typography>
                            <Typography variant='caption' sx={{ ml: 1 }}>
                                {option.description}
                            </Typography>
                        </Stack>
                    </li>
                );
            }}
        />
    );
};

export default ItemAutocomplete;
