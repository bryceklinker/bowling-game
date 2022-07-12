import React from 'react';
import {createRoot} from 'react-dom/client';
import './index.css';
import {BrowserRouter, Routes, Route, Navigate} from 'react-router-dom';
import {Shell} from './shell/Shell';
import {NewGame} from './new-game/NewGame';
import {AppProviders} from './shared/components/AppProviders';
import {QueryClient} from 'react-query';

const client = new QueryClient();
const element = document.getElementById('root');
const root = createRoot(element!);
root.render(
    <React.StrictMode>
        <AppProviders client={client}>
            <BrowserRouter>
                <Routes>
                    <Route path={'/'} element={<Shell/>}>
                        <Route path={'new-game'} element={<NewGame/>}/>
                        <Route index element={<Navigate to={'/new-game'}/>}/>
                    </Route>
                </Routes>
            </BrowserRouter>
        </AppProviders>
    </React.StrictMode>
);
