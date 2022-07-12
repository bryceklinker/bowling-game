import {FieldValues, useForm, UseFormProps} from 'react-hook-form';
import {UseFormReturn} from 'react-hook-form/dist/types';
import {SchemaOf} from 'yup';
import {yupResolver} from './yup-resolver';

export type UseValidationForm<TFieldValues extends FieldValues = FieldValues, TContext extends object = object> = Omit<UseFormProps<TFieldValues, TContext>, 'resolver'>
& {schema: SchemaOf<TFieldValues>};

export function useValidationForm<TFieldValues extends FieldValues = FieldValues, TContext extends object = object>(options: UseValidationForm<TFieldValues, TContext>): UseFormReturn<TFieldValues, TContext> {
    return useForm<TFieldValues, TContext>({
        ...options,
        resolver: yupResolver(options.schema)
    });
}
