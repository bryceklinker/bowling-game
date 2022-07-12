import {FCWithChildren} from '../types/FCWithChildren';
import {QueryClient, QueryClientProvider} from 'react-query';
import {RestApiProvider} from '../rest-api/rest-api-context';
import {ConfigProvider} from '../config/config-context';
import {BowlingGameThemeProvider} from '../theming/BowlingGameTheme';
import {useMemo} from 'react';

export type AppProvidersProps = {
    client: QueryClient;
}

export const AppProviders: FCWithChildren<AppProvidersProps> = ({children, client}) => {
    return (
        <QueryClientProvider client={client}>
            <BowlingGameThemeProvider>
                <ConfigProvider>
                    <RestApiProvider>
                        {children}
                    </RestApiProvider>
                </ConfigProvider>
            </BowlingGameThemeProvider>
        </QueryClientProvider>
    );
};
