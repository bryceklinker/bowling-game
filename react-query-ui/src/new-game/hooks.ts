import {useMutation} from 'react-query';
import {GameModel} from '../shared/models/game-model';
import {useRestApi} from '../shared/rest-api/rest-api-context';

export function useNewGame(success: (game: GameModel) => void) {
    const restApi = useRestApi();
    return useMutation(
        ['games'],
        async () => {
            const response = await restApi.post<GameModel>('/new-game');
            return response.data;
        },
        {onSuccess: success}
    );
}
