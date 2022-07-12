import * as Yup from 'yup';
import {SchemaOf} from 'yup';

export type PlayerModel = {
    id: string;
    name: string;
}

export type CreatePlayerModel = Omit<PlayerModel, 'id'>;

export const CreatePlayerValidation: SchemaOf<CreatePlayerModel> = Yup.object({
    name: Yup.string().required()
})
