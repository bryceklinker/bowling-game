import {FC} from 'react';
import {ColumnFlexBox} from '../shared/components/ColumnFlexBox';
import {useParams} from 'react-router';
import {useGamePlayers} from './hooks';
import {GamePlayer} from './GamePlayer';
import {BowlingGameLoading} from '../shared/components/BowlingGameLoading';
import {Typography} from '@mui/material';

export const Game: FC = () => {
    const {gameId} = useParams<{ gameId: string }>();
    const {data: players, isLoading} = useGamePlayers(gameId);

    if (isLoading) {
        return (
            <BowlingGameLoading aria-label={'loading players'}>
                <Typography>Loading Players...</Typography>
            </BowlingGameLoading>
        );
    }

    const items = (players || []).map(p => <GamePlayer key={p.id} player={p}/>);
    return (
        <ColumnFlexBox>
            {items}
        </ColumnFlexBox>
    );
};
