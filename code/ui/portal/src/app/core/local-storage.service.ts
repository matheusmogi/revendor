import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class LocalStorageService {

    constructor() { }

    get(key: string, shouldValidateExpiration = true): string {
        const itemString = localStorage.getItem(key);
        if (!itemString || itemString === 'undefined') {
            return null;
        }

        const item = JSON.parse(itemString);

        if (shouldValidateExpiration) {
            const now = new Date();
            if (now.getTime() > item.expiry) {
                localStorage.removeItem(key);
                return null;
            }
            return item.value;
        }
        return item.value;

    }

    set(key: string, value: string): void {
        const now = new Date();

        const item = {
            value: value,
            expiry: now.getTime() + 7200000,
        };
        localStorage.setItem(key, JSON.stringify(item));
    }
}
