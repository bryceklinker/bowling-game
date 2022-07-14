import {FieldValues, useForm, UseFormProps, UseFormReturn} from 'react-hook-form';
import {yupResolver} from '@hookform/resolvers/yup';
import {SchemaOf} from 'yup';

export type UseValidationForm<TFieldValues extends FieldValues = FieldValues, TContext extends object = object> = Omit<UseFormProps<TFieldValues, TContext>, 'resolver'>
& {schema: SchemaOf<TFieldValues>};

export function useValidationForm<TFieldValues extends FieldValues = FieldValues, TContext extends object = object>(options: UseValidationForm<TFieldValues, TContext>): UseFormReturn<TFieldValues, TContext> {
    return useForm<TFieldValues, TContext>({
        ...options,
        resolver: yupResolver(options.schema)
    });
}
