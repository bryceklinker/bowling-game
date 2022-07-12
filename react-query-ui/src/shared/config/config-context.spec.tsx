import {TestingApi} from '../../../testing/testing-api';
import {ConfigProvider} from './config-context';
import {Typography} from '@mui/material';
import {screen, waitFor} from '@testing-library/dom';
import {render} from '@testing-library/react';
import {QueryClient, QueryClientProvider} from 'react-query';

describe('ConfigProvider', () => {
    test('when rendered then shows loading when config is being loaded', async () => {
        TestingApi.setupConfig(undefined, {delay: 1000});

        render(
            <QueryClientProvider client={new QueryClient()}>
                <ConfigProvider>
                    <Typography aria-label={'hello'}></Typography>
                </ConfigProvider>
            </QueryClientProvider>
        );

        await waitFor(() => expect(screen.getByRole('progressbar')).toBeInTheDocument());
    });

    test('when config is loaded then shows children', async () => {
        render(
            <QueryClientProvider client={new QueryClient()}>
                <ConfigProvider>
                    <Typography aria-label={'hello'}></Typography>
                </ConfigProvider>
            </QueryClientProvider>
        );

        await waitFor(() => expect(screen.getByLabelText('hello')).toBeInTheDocument());
    });
});
