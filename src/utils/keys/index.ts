import type { AxiosInstance } from 'axios';
import type { InjectionKey } from 'vue';
import type { RouteLocationNormalizedLoaded } from 'vue-router';

export const AxiosKey: InjectionKey<AxiosInstance> = Symbol();
export const RouterKey: InjectionKey<RouteLocationNormalizedLoaded> = Symbol();
