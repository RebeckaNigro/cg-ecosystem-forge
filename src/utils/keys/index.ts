import { InjectionKey } from "vue"
import { AxiosInstance } from "axios"
import { RouteLocationNormalizedLoaded } from 'vue-router';

export const AxiosKey: InjectionKey<AxiosInstance> = Symbol()
export const RouterKey: InjectionKey<RouteLocationNormalizedLoaded> = Symbol()