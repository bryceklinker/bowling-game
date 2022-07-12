import {useMutation, useQuery} from 'react-query';
import {useRestApi} from '../shared/rest-api/rest-api-context';
import {CreatePlayerModel, PlayerModel} from '../shared/models/player-model';

export function useGamePlayers(gameId?: string) {
    const restApi = useRestApi();
    return useQuery([gameId, 'players'], async () => {
        const response = await restApi.get<Array<PlayerModel>>(`/games/${gameId}/players`);
        return response.data;
    }, {enabled: Boolean(gameId)});
}

export function useSaveGamePlayer(gameId?: string, onSuccess?: (player: PlayerModel) => void) {
    const restApi = useRestApi();
    return useMutation([gameId, 'players'], async (player: CreatePlayerModel) => {
        const response = await restApi.post<PlayerModel>(`/games/${gameId}/players`, player);
        return response.data;
    }, {onSuccess});
}
