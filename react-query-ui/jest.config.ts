import path from 'path';

module.exports = {
    preset: 'ts-jest',
    restoreMocks: true,
    resetMocks: true,
    collectCoverage: true,
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
