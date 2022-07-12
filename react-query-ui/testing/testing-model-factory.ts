import {GameModel} from '../src/shared/models/game-model';
import {faker} from '@faker-js/faker';
import {PlayerModel} from '../src/shared/models/player-model';

function createGame(model: Partial<GameModel> = {}): GameModel {
    return {
        id: faker.datatype.uuid(),
        ...model,
    }
}

function createPlayer(model: Partial<PlayerModel> = {}): PlayerModel {
    return {
        id: faker.datatype.uuid(),
        name: faker.name.firstName(),
        ...model
    }
}

function createMany<T>(creator: () => T, count = 3): Array<T> {
    const items = [];
    for (let i = 0; i < count; i++) {
        items.push(creator());
    }
    return items;
}

export const TestingModelFactory = {
    createGame,
    createPlayer,
    createMany,
}
