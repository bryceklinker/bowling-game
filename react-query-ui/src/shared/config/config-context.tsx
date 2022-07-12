import {createContext, useContext, useState} from 'react';
import {ConfigModel} from '../models/config-model';
import {FCWithChildren} from '../types/FCWithChildren';
import {useQuery} from 'react-query';
import axios from 'axios';
import {AppLoading} from '../components/AppLoading';

type ConfigContextProps = {
    config: ConfigModel | null;
}

const ConfigContext = createContext<ConfigContextProps>({config: null});

export function useConfig() {
    const {config} = useContext(ConfigContext);
    if (!config) {
        throw new Error(`You must use ${useConfig.name} inside a ${ConfigProvider.name}`)
    }

    return config;
}

export const ConfigProvider: FCWithChildren = ({children}) => {
    const {data} = useQuery(['config'], async () => {
        const response = await axios.get<ConfigModel>('/config');
        return response.data;
    });

    if (!data) {
        return <AppLoading />
    }

    return (
        <ConfigContext.Provider value={{config: data}}>
            {children}
        </ConfigContext.Provider>
    )
}
