import { styled } from '@mui/system';
import { Pagination } from '@mui/material';

export const StyledPagination = styled(Pagination)(
    {
        '& .MuiPaginationItem-root': {
            color: 'primary.main',
        },
        '& .MuiPaginationItem-root.Mui-selected': {
            backgroundColor: 'primary.main',
            color: 'black',
        }
    });