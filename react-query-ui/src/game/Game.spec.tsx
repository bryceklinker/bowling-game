import {TestingApi} from '../../testing/testing-api';
import {TestingModelFactory} from '../../testing/testing-model-factory';
import {renderForTest} from '../../testing/render-for-test';
import {Game} from './Game';
import {screen, waitFor} from '@testing-library/dom';

describe('Game', () => {
    test('when rendered then shows players', async () => {
        TestingApi.setupGetPlayers('123', TestingModelFactory.createMany(TestingModelFactory.createPlayer, 5));

        await renderForTest(<Game/>, {currentRoute: '/games/123', routePath: '/games/:gameId'});

        await waitFor(() => expect(screen.getAllByLabelText('player')).toHaveLength(5));
    });

    test('when players are loading then shows loading players', async () => {
        TestingApi.setupGetPlayers('444', [], {delay: 500});

        await renderForTest(<Game/>, {currentRoute: '/games/444', routePath: '/games/:gameId'});

        await waitFor(() => expect(screen.getByRole('progressbar', {name: 'loading players'})).toBeInTheDocument());
    });
});
