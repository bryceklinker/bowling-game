import {FC, useCallback, useMemo} from 'react';
import {Dialog, DialogTitle, DialogActions, DialogContent, Button, TextField} from '@mui/material';
import {Controller} from 'react-hook-form';
import {CreatePlayerModel, CreatePlayerValidation} from '../shared/models/player-model';
import {useSaveGamePlayer} from './hooks';
import {useParams} from 'react-router';
import {useValidationForm} from '../shared/validation/use-validation-form';

export type AddPlayerToGameDialogProps = {
    open: boolean;
    onClose?: () => void;
}

export const AddPlayerToGameDialog: FC<AddPlayerToGameDialogProps> = ({open, onClose}) => {
    const {gameId} = useParams<{ gameId: string }>();
    const {handleSubmit, control, formState} = useValidationForm<CreatePlayerModel>({
        defaultValues: {name: ''},
        schema: CreatePlayerValidation
    });
    const {mutate, isLoading} = useSaveGamePlayer(gameId, onClose);
    const onSubmit = useCallback((player: CreatePlayerModel) => mutate(player), [mutate]);
    const canSave = useMemo(() => !isLoading && formState.isValid, [isLoading, formState.isValid]);
    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Add Player</DialogTitle>
            <DialogContent>
                <Controller control={control}
                            name={'name'}
                            render={({field}) => (
                                <TextField {...field} label={'Name'}/>
                            )}
                />
            </DialogContent>
            <DialogActions>
                <Button aria-label={'save player'} onClick={handleSubmit(onSubmit)} disabled={!canSave}>Submit</Button>
                <Button aria-label={'cancel player'} onClick={onClose} disabled={isLoading}>Cancel</Button>
            </DialogActions>
        </Dialog>
    );
};
