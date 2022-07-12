import {Typography} from '@mui/material';
import {useRestApi} from './rest-api-context';
import {TestingApi} from '../../../testing/testing-api';
import {renderForTest} from '../../../testing/render-for-test';
import {screen, waitFor} from '@testing-library/dom';

describe('RestApiProvider', () => {
    test('when rendered then base url for api is set', async () => {
        TestingApi.setupConfig({apiUrl: 'https://google.com'});

        await renderForTest(<TestingComponent/>);

        await waitFor(() => expect(screen.getByLabelText('base url')).toHaveTextContent('https://google.com'))
    });

    function TestingComponent() {
        const restApi = useRestApi();
        return (
            <Typography aria-label={'base url'}>{restApi.defaults.baseURL}</Typography>
        )
    }
});
