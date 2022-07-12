import {createTheme, ThemeProvider, PaletteMode, Theme, CssBaseline} from '@mui/material';
import {createContext, FC, useMemo, useState} from 'react';
import {FCWithChildren} from '../types/FCWithChildren';


type BowlingGameThemeContextProps = {mode: PaletteMode, changeMode: (mode: PaletteMode) => void};
const BowlingGameThemeContext = createContext<BowlingGameThemeContextProps>({
    mode: 'dark',
    changeMode: () => {}
});


function createBowlingGameTheme(mode: PaletteMode): Theme {
    return createTheme({
        palette: {
            mode
        }
    })
}

export const BowlingGameThemeProvider: FCWithChildren = ({children}) => {
    const [paletteMode, setPaletteMode] = useState<PaletteMode>('dark');
    const theme = useMemo(() => createBowlingGameTheme(paletteMode), [paletteMode]);
    const value = useMemo<BowlingGameThemeContextProps>(
        () => ({mode: paletteMode, changeMode: setPaletteMode}),
        [paletteMode, setPaletteMode]
    );
    return (
        <BowlingGameThemeContext.Provider value={value}>
            <ThemeProvider theme={theme}>
                <>
                    <CssBaseline />
                    {children}
                </>
            </ThemeProvider>
        </BowlingGameThemeContext.Provider>

    )
}
