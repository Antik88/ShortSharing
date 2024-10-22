import Grid from '@mui/material/Grid2';
import FileUpload from './FileUpload';
import FilePreviewList from './FilePreviewList';
import { FileWithURL } from '../types/types';

interface FileSectionProps {
  files: FileWithURL[];
  onFilesSelected: (files: FileWithURL[]) => void;
  onDelete: (file: FileWithURL) => void;
}

export default function FileSection ({ files, onFilesSelected, onDelete }: FileSectionProps) {
  return (
    <Grid size={12} border={1} borderRadius={1} padding={2}>
      <FileUpload onFilesSelected={onFilesSelected} />
      <FilePreviewList files={files} onDelete={onDelete} />
    </Grid>
  );
};
