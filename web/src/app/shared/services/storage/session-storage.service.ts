import { Injectable } from '@angular/core';

@Injectable()
export class SessionStorageService {

    public set = (key: string, value: any) => {
        sessionStorage.setItem(key, JSON.stringify(value));
    }

    public get = (key: string): any => {
        return sessionStorage.getItem(key);
    }

    public remove = (key: string) => {
        return sessionStorage.removeItem(key);
    }
}
