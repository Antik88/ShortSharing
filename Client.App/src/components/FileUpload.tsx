import React, { useRef, useCallback } from 'react';
import { FileWithURL } from '../types/types';
import { VisuallyHiddenInput } from '../styled/VisuallyHiddenInput';
import { StyledButton } from '../styled/StyledButton';

interface FileUploadProps {
    onFilesSelected: (files: FileWithURL[]) => void;
}

export default function FileUpload({ onFilesSelected }: FileUploadProps) {
    const inputRef = useRef<HTMLInputElement>(null);

    const handleFileChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files) {
            const selectedFiles: FileWithURL[] = Array.from(event.target.files).map(file => ({
                file,
                url: URL.createObjectURL(file)
            }));

            onFilesSelected(selectedFiles);

            if (inputRef.current) {
                inputRef.current.value = '';
            }
        }
    }, [onFilesSelected]);

    return (
        <StyledButton component="label">
            Upload Image
            <VisuallyHiddenInput
                type="file"
                onChange={handleFileChange}
                multiple
                ref={inputRef}
            />
        </StyledButton>
    );
};
