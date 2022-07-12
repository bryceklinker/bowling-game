import {render} from '@testing-library/react';
import {QueryClient, QueryClientProvider} from 'react-query';
import {MemoryRouter, MemoryRouterProps, Route, Routes} from 'react-router';
import {BowlingGameThemeProvider} from '../src/shared/theming/BowlingGameTheme';
import {ConfigProvider} from '../src/shared/config/config-context';
import {RestApiProvider} from '../src/shared/rest-api/rest-api-context';
import {AppProviders} from '../src/shared/components/AppProviders';
import {screen, waitFor} from '@testing-library/dom';

export type TestingRenderOptions = MemoryRouterProps
    & { routePath?: string, currentRoute?: string };

export async function renderForTest(ui: JSX.Element, {currentRoute, routePath, ...options}: TestingRenderOptions = {}) {
    const client = new QueryClient({
        defaultOptions: {
            queries: {
                retry: false
            },
            mutations: {
                retry: false
            }
        }
    })
    const initialEntries = currentRoute ? [currentRoute] : options.initialEntries;
    const result = render(
        <AppProviders client={client}>
            <MemoryRouter initialEntries={initialEntries ?? ['/']} {...options}>
                <Routes>
                    <Route path={routePath ?? '/'} element={ui}/>
                </Routes>
            </MemoryRouter>
        </AppProviders>
    );
    await waitFor(() => expect(screen.queryByRole('progressbar', {name: 'application loading'})).not.toBeInTheDocument());
    return result;
}
