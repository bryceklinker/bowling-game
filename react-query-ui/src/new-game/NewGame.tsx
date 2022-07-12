import {Button, Icon, Typography} from '@mui/material';
import ErrorIcon from '@mui/icons-material/Error';
import {FC, useCallback} from 'react';
import {useNewGame} from './hooks';
import {GameModel} from '../shared/models/game-model';
import {useNavigate} from 'react-router';
import {BowlingGameLoading} from '../shared/components/BowlingGameLoading';
import {ColumnFlexBox} from '../shared/components/ColumnFlexBox';
import {RowFlexBox} from '../shared/components/RowFlexBox';

export const NewGame: FC = () => {
    const navigate = useNavigate();
    const handleGameCreated = useCallback((game: GameModel) => {
        navigate(`/games/${game.id}`);
    }, [navigate]);
    const {isLoading, error, mutate} = useNewGame(handleGameCreated);
    return (
        <ColumnFlexBox>
            <>
                <Button aria-label={'start game'} onClick={() => mutate()} disabled={isLoading}>
                    Start New Game &nbsp; {!isLoading && <BowlingGameLoading size={'1.5em'}/>}
                </Button>
                {
                    error &&
                    <RowFlexBox aria-label={'error'}>
                        <>
                            <Typography color={'error'}>Failed to create new game</Typography>
                            <Icon color={'error'}>
                                <ErrorIcon/>
                            </Icon>
                        </>
                    </RowFlexBox>
                }
            </>
        </ColumnFlexBox>
    );
};
