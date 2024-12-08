import { Button } from '@mui/material';

const UploadingButton = ({
    onUpload,
    allowedContentTypes,
    onErrorContentType,
}) => {
    const handleUpload = (file) => {
        if (allowedContentTypes.every((type) => type !== file.type)) {
            onErrorContentType?.();
            return;
        }
        onUpload(file);
    };

    return (
        <Button variant='text' component='label'>
            Upload cover
            <input
                type='file'
                hidden
                onChange={(event) => {
                    handleUpload(event.target.files[0]);
                }}
            ></input>
        </Button>
    );
};

export default UploadingButton;
