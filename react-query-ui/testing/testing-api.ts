import {setupServer} from 'msw/node';
import {ResponseComposition, rest, RestContext, RestRequest} from 'msw';
import {constants} from 'http2';
import {TestingModelFactory} from './testing-model-factory';
import {GameModel} from '../src/shared/models/game-model';
import {PlayerModel} from '../src/shared/models/player-model';
import {ConfigModel} from '../src/shared/models/config-model';

export type SetupApiOptions = Partial<{
    status: number;
    delay: number;
}>;

const BASE_URL = 'https://localhost:5001';
let server = setupServer();

function start() {
    server.listen({
        onUnhandledRequest: 'error'
    });
}

function reset() {
    server.resetHandlers();
    setupConfig();
    setupNewGame();
    setupGetPlayers('*');
    setupAddPlayer('*');
}

function stop() {
    server.close();
}

function createHandler<T>(result?: T, options?: SetupApiOptions) {
    const delay = options?.delay || 0;
    const status = options?.status || result ? constants.HTTP_STATUS_OK : constants.HTTP_STATUS_NO_CONTENT;
    return (req: RestRequest, res: ResponseComposition, ctx: RestContext) => {
        return res(
            ctx.delay(delay),
            ctx.status(status),
            ctx.json(result)
        )
    }
}

function setupPost<T = unknown>(path: string, result?: T, options?: SetupApiOptions) {
    server.use(
        rest.post(`${BASE_URL}${path}`, createHandler(result, options))
    )
}

function setupGet<T = unknown>(path: string, result?: T, options?: SetupApiOptions) {
    server.use(
        rest.get(`${BASE_URL}${path}`, createHandler(result, options))
    )
}

function setupNewGame(game?: GameModel | null, options?: SetupApiOptions) {
    const result = game === undefined ? TestingModelFactory.createGame() : game;
    setupPost('/new-game', result, options);
}

function setupGetPlayers(gameId: string, players?: Array<PlayerModel>, options?: SetupApiOptions) {
    const result = players ?? TestingModelFactory.createMany(TestingModelFactory.createPlayer);
    setupGet(`/games/${gameId}/players`, result, options);
}

function setupAddPlayer(gameId: string, player?: PlayerModel, options?: SetupApiOptions) {
    const result = player ?? TestingModelFactory.createPlayer();
    setupPost(`/games/${gameId}/players`, player, options);
}

function setupConfig(config?: ConfigModel, options?: SetupApiOptions) {
    const result = config ?? {apiUrl: BASE_URL};
    server.use(
        rest.get('/config', createHandler<ConfigModel>(result, options))
    )
}

export const TestingApi = {
    start,
    stop,
    reset,
    setupPost,
    setupGet,
    setupConfig,
    setupNewGame,
    setupGetPlayers,
    setupAddPlayer
}
