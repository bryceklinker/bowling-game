import {renderForTest} from '../../testing/render-for-test';
import {AddPlayerToGameDialog} from './AddPlayerToGameDialog';
import userEvent from '@testing-library/user-event';
import {screen, waitFor} from '@testing-library/dom';

describe('AddPlayerToGameDialog', () => {
    test('when player is added then closes modal', async () => {
        const onClose = jest.fn();

        await renderForTest(<AddPlayerToGameDialog open={true} onClose={onClose}/>, {
            routePath: '/games/:gameId',
            currentRoute: '/games/555'
        });

        await userEvent.type(screen.getByLabelText('Name'), 'Bob');
        await waitFor(() => expect(screen.getByRole('button', {name: 'save player'})).not.toBeDisabled());
        await userEvent.click(screen.getByRole('button', {name: 'save player'}));

        await waitFor(() => expect(onClose).toHaveBeenCalled());
    }, 10000);

    test('when cancelled then closes modal', async () => {
        const onClose = jest.fn();

        await renderForTest(<AddPlayerToGameDialog open={true} onClose={onClose}/>, {
            routePath: '/games/:gameId',
            currentRoute: '/games/555'
        });
        await userEvent.click(screen.getByRole('button', {name: 'cancel player'}));

        await waitFor(() => expect(onClose).toHaveBeenCalled());
    });

    test('when invalid then save is disabled', async () => {
        await renderForTest(<AddPlayerToGameDialog open={true}/>, {
            routePath: '/games/:gameId',
            currentRoute: '/games/555'
        });

        await waitFor(() => expect(screen.getByRole('button', {name: 'save player'})).toBeDisabled());
    })
});
