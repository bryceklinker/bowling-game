import {FC} from 'react';
import {PlayerModel} from '../shared/models/player-model';
import {RowFlexBox} from '../shared/components/RowFlexBox';

export type GamePlayerProps = {
    player: PlayerModel;
}
export const GamePlayer: FC<GamePlayerProps> = ({player}) => {
    return (
        <RowFlexBox aria-label={'player'}>

        </RowFlexBox>
    );
}
