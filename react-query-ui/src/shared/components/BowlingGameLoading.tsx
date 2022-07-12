import {FC, ReactNode} from 'react';
import {CircularProgress, CircularProgressProps} from '@mui/material';
import {ColumnFlexBox} from './ColumnFlexBox';

export type BowlingGameLoadingProps = CircularProgressProps & {children?: ReactNode};

export const BowlingGameLoading: FC<BowlingGameLoadingProps> = ({children, ...props}) => {
    return (
        <ColumnFlexBox justifyContent={'center'} alignItems={'center'}>
            <CircularProgress {...props}/>
            {children}
        </ColumnFlexBox>

    )
}
