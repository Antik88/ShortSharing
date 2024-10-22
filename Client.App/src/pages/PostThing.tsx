import { useEffect, useState, useCallback } from 'react';
import { Container, Typography, Snackbar, Alert, TextField, Select, MenuItem, InputLabel, FormControl } from '@mui/material';
import Grid from '@mui/material/Grid2';
import { getCategories, postThing, putImage } from '../http/catalogAPI';
import { Category, CategoryType, FileWithURL } from '../types/types';
import { useNavigate } from 'react-router-dom';
import { THING_ROUTE } from '../utils/consts';
import FileSection from '../components/FileSection';
import { StyledButton } from '../styled/StyledButton';
import useUserStore from '../store/useUserStore';

export default function PostThingPage() {
    const [files, setFiles] = useState<FileWithURL[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    const [selectedCategory, setSelectedCategory] = useState<Category | null>(null);
    const [selectedType, setSelectedType] = useState<CategoryType | null>(null);
    const [name, setName] = useState<string>('');
    const [price, setPrice] = useState<number | string>('');
    const [description, setDescription] = useState<string>('');
    const [showSnackbar, setShowSnackbar] = useState(false);
    const navigate = useNavigate();
    const user = useUserStore();

    useEffect(() => {
        const fetchCategories = async () => {
            const data = await getCategories();
            setCategories(data);
        };
        fetchCategories();
    }, []);

    const handleFilesSelected = useCallback((selectedFiles: FileWithURL[]) => {
        const existingFiles = new Set(files.map(file => file.file.name));
        const uniqueFiles = selectedFiles.filter(newFile => !existingFiles.has(newFile.file.name));

        if (uniqueFiles.length < selectedFiles.length) {
            setShowSnackbar(true);
        }

        if (uniqueFiles.length > 0) {
            setFiles(prevFiles => [...prevFiles, ...uniqueFiles]);
        }
    }, [files]);

    const handleDeleteFile = useCallback((fileToDelete: FileWithURL) => {
        setFiles(prevFiles => {
            URL.revokeObjectURL(fileToDelete.url);
            return prevFiles.filter(f => f.file !== fileToDelete.file);
        });
    }, []);

    const uploadImages = async (thingId: string) => {
        for (const fileWithUrl of files) {
            await putImage(thingId, fileWithUrl.file);
        }
    };

    const createThing = async () => {
        if (!selectedCategory || !selectedType || name === '' || price === '') {
            setShowSnackbar(true);
            return;
        }

        const thing = await postThing({
            id: '',
            name: name,
            price: price,
            description: description,
            categoryId: selectedCategory.id.toString(),
            typeId: selectedType.id.toString(),
            ownerId: user.user.id?.toString(),
        });

        await uploadImages(thing.id);
        navigate(THING_ROUTE + `/${thing.id}`);
    };

    return (
        <Container sx={{ mt: 2 }}>
            <Typography variant="h4" color="primary" marginBottom='20px'>
                Post thing
            </Typography>
            <Grid container spacing={2}>
                <TextField autoComplete='off' label="Name" variant="outlined" value={name} onChange={(e) => setName(e.target.value)} />
                <TextField autoComplete='off' label="Price" variant="outlined" type='number' value={price} onChange={(e) => setPrice(e.target.value)} />
                <TextField sx={{ mb: 2 }} fullWidth multiline rows={4} label="Description" variant="outlined" value={description} onChange={(e) => setDescription(e.target.value)} />
            </Grid>
            <Grid container spacing={2}>
                <FormControl variant="outlined" sx={{ width: 150 }}>
                    <InputLabel>Category</InputLabel>
                    <Select
                        label="Category"
                        value={selectedCategory ? selectedCategory.id : ''}
                        onChange={(e) => {
                            const categoryId = e.target.value;
                            const category = categories.find(c => c.id === categoryId);
                            setSelectedCategory(category || null);
                            setSelectedType(null);
                        }}
                    >
                        {categories.map(category => (
                            <MenuItem key={category.id.toString()} value={category.id.toString()}>
                                {category.name}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
                <FormControl variant="outlined" sx={{ width: 150 }}>
                    <InputLabel>Type</InputLabel>
                    <Select
                        label="Type"
                        value={selectedType ? selectedType.id : ''}
                        onChange={(e) => {
                            const typeId = e.target.value;
                            const selectedCategoryTypes = selectedCategory?.types || [];
                            const type = selectedCategoryTypes.find(t => t.id === typeId);
                            setSelectedType(type || null);
                        }}
                        disabled={!selectedCategory}
                    >
                        {selectedCategory?.types.map(type => (
                            <MenuItem key={type.id.toString()} value={type.id.toString()}>
                                {type.name}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
            </Grid>
            <Grid container spacing={2} sx={{ mt: 2 }}>
                <FileSection
                    files={files}
                    onFilesSelected={handleFilesSelected}
                    onDelete={handleDeleteFile}
                />
                <StyledButton color="primary" variant="outlined" onClick={createThing}>
                    Create
                </StyledButton>
            </Grid>
            <Snackbar
                open={showSnackbar}
                autoHideDuration={3000}
                onClose={() => setShowSnackbar(false)}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
            >
                <Alert onClose={() => setShowSnackbar(false)} severity="warning" sx={{ width: '100%' }}>
                    Something went wrong!
                </Alert>
            </Snackbar>
        </Container>
    );
}
