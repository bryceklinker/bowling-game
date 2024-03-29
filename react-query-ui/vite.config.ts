/// <reference types="vitest" />
/// <reference types="vite/client" />
import {defineConfig} from 'vite';
import react from '@vitejs/plugin-react';
import svgr from 'vite-plugin-svgr';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [react(), svgr()],
    test: {
        globals: true,
        environment: 'jsdom',
        setupFiles: [
            path.resolve(__dirname, 'testing', 'vitest.setup.ts')
        ]
    }
});
