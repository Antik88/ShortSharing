import { styled } from '@mui/system';
import { Button, ButtonProps } from '@mui/material';

export const StyledButton = styled(Button)<ButtonProps>(
  {
    color: "primary",
    border: '3px solid',
    borderRadius: '0',
    variant: "outlined",
  });