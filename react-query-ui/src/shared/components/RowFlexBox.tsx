import {FC} from 'react';
import {Box, BoxProps} from '@mui/material';

export const RowFlexBox: FC<BoxProps> = (props) => <Box display={'flex'} flex={1} flexDirection={'row'} {...props}/>
