import {TestingApi} from './testing-api';

import '@testing-library/jest-dom';

jest.mock('react-router-dom', () => {
    const navigate = jest.fn();
    return {
        ...jest.requireActual('react-router-dom'),
        useNavigate: () => navigate
    };
});

jest.mock('react-router', () => {
    const navigate = jest.fn();
    return {
        ...jest.requireActual('react-router'),
        useNavigate: () => navigate
    };
});
beforeAll(() => {
    TestingApi.start();
});

beforeEach(() => {
    TestingApi.reset();
});

afterAll(() => {
    TestingApi.stop();
});
