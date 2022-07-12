import {FC} from 'react';
import {BowlingGameLoading} from './BowlingGameLoading';
import {Typography} from '@mui/material';

export const AppLoading: FC = () => {
    return (
        <BowlingGameLoading size={'6em'} aria-label={'application loading'}>
            <Typography variant={'h3'}>Application is loading...</Typography>
        </BowlingGameLoading>
    );
};
