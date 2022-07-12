import {screen, waitFor} from '@testing-library/dom';
import userEvent from '@testing-library/user-event';
import {useNavigate} from 'react-router';
import {NewGame} from './NewGame';
import {renderForTest} from '../../testing/render-for-test';
import {TestingModelFactory} from '../../testing/testing-model-factory';
import {TestingApi} from '../../testing/testing-api';
import {constants} from 'http2';

describe('NewGame', () => {
    test('when new game is started then tells api to start new game', async () => {
        const navigate = useNavigate();
        const game = TestingModelFactory.createGame();
        TestingApi.setupNewGame(game);

        await renderForTest(<NewGame/>);

        await clickStartGame();

        await waitFor(() => expect(navigate).toHaveBeenCalledWith(`/games/${game.id}`));
    });

    test('when new game started then shows loading while starting new game', async () => {
        TestingApi.setupNewGame(undefined, {delay: 500});

        await renderForTest(<NewGame/>);

        await clickStartGame();

        await waitFor(() => expect(screen.getByRole('button', {name: 'start game'})).toBeDisabled());
        await waitFor(() => expect(screen.queryByRole('progressbar')).toBeInTheDocument());
    });

    test('when new game fails to create then shows error', async () => {
        TestingApi.setupNewGame(null, {status: constants.HTTP_STATUS_INTERNAL_SERVER_ERROR});

        await renderForTest(<NewGame />);
        await clickStartGame();

        await waitFor(() => expect(screen.getByLabelText('error')).toBeInTheDocument());
    })

    async function clickStartGame() {
        await userEvent.click(screen.getByRole('button', {name: 'start game'}));
    }
});
