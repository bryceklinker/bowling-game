const path = require('path');
module.exports = {
    preset: 'ts-jest',
    restoreMocks: true,
    resetMocks: true,
    coverage: true,
    testEnvironment: 'jsdom',
    setupFilesAfterEnv: [
        '<rootDir>/testing/jest.setup.ts'
    ],
    globals: {
        'ts-jest': {
            tsconfig: path.resolve(__dirname, 'tsconfig.spec.json')
        }
    }
};
