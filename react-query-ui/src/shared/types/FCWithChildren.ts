import {FC, ReactNode} from 'react';

export type FCWithChildren<Props = {}> = FC<Props & { children: ReactNode }>;
