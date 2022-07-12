import {FC} from 'react';
import {Box, BoxProps} from '@mui/material';

export const ColumnFlexBox: FC<BoxProps> = (props) => <Box display={'flex'} flex={1} flexDirection={'column'} {...props}/>
