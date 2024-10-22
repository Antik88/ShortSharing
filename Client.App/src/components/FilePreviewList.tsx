import { Box, IconButton } from "@mui/material";
import DeleteIcon from '@mui/icons-material/Delete';
import { FileWithURL } from '../types/types';

interface FilePreviewListProps {
    files: FileWithURL[];
    onDelete: (file: FileWithURL) => void;
}

export default function FilePreviewList({ files, onDelete } : FilePreviewListProps) {
    return (
        <Box sx={{ mt: 2, display: 'flex', flexWrap: 'wrap', gap: 2 }}>
            {files.map((fileWithURL) => (
                <Box
                    key={`${fileWithURL.file.name}-${fileWithURL.file.lastModified}`}
                    sx={{ position: 'relative', width: 100, height: 100 }}
                >
                    <img
                        src={fileWithURL.url}
                        alt={fileWithURL.file.name}
                        style={{ width: '100%', height: '100%', objectFit: 'cover' }}
                    />
                    <IconButton
                        sx={{
                            position: 'absolute',
                            top: 0,
                            right: 0,
                            color: 'white',
                            backgroundColor: 'rgba(0, 0, 0, 0.5)',
                            '&:hover': {
                                backgroundColor: 'rgba(0, 0, 0, 0.7)',
                            },
                        }}
                        onClick={() => onDelete(fileWithURL)}
                        aria-label={`Delete ${fileWithURL.file.name}`}
                    >
                        <DeleteIcon />
                    </IconButton>
                </Box>
            ))}
        </Box>
    );
};
