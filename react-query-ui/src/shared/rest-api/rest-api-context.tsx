import {createContext, useContext, useMemo, useState} from 'react';
import axios, {AxiosInstance} from 'axios';
import {FCWithChildren} from '../types/FCWithChildren';
import {useQuery} from 'react-query';
import {useConfig} from '../config/config-context';

type RestApiContextProps = {
    restApi: AxiosInstance | null;
}
const RestApiContext = createContext<RestApiContextProps>({restApi: null});

export function useRestApi() {
    const {restApi} = useContext(RestApiContext);
    if (!restApi) {
        throw new Error(`You must use ${useRestApi.name} inside a ${RestApiProvider.name}`);
    }

    return restApi;
}

export const RestApiProvider: FCWithChildren = ({children}) => {
    const config = useConfig();
    const restApi = useMemo(() => axios.create({baseURL: config.apiUrl}), [config]);
    return (
        <RestApiContext.Provider value={{restApi}}>
            {children}
        </RestApiContext.Provider>
    )
}
