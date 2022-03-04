/* eslint-disable guard-for-in */
/* eslint-disable @typescript-eslint/ban-types */
/* eslint-disable @typescript-eslint/explicit-function-return-type */
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { Subject, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ApiService {

    public onError: Subject<string> = new Subject<string>();

    constructor(private http: HttpClient) { }

    get<T>(endpoint: string, params: Object = {}) {
        return this.http.get(this.getURL(endpoint, params))
            .pipe(map((response: T) => response),
                catchError((error) => {
                    this.onError.next(error.message);
                    return throwError(error);
                })
            );
    }

    post<T>(endpoint: string, params: Object = {}, data: Object = {}) {
        return this.http.post(this.getURL(endpoint, params), data)
            .pipe(map((response: T) => response),
                catchError((error) => {
                    this.onError.next(error.message);
                    return throwError(error);
                })
            );
    }

    patch<T>(endpoint: string, params: Object = {}, data: Object = {}) {
        return this.http.patch(this.getURL(endpoint, params), data)
            .pipe(map((response: T) => response),
                catchError((error) => {
                    this.onError.next(error.message);
                    return throwError(error);
                })
            );
    }

    delete<T>(endpoint: string, params: Object = {}, data: Object = {}) {
        return this.http.delete(this.getURL(endpoint, params), data)
            .pipe(map((response: T) => response),
                catchError((error) => {
                    this.onError.next(error.message);
                    return throwError(error);
                })
            );
    }

    getURL(endpoint: string, params: Object) {
        return this.getParams(endpoint, params);
    }

    getParams(url: string, params: Object) {
        for (const key in params) {
            url = url.replace(new RegExp('\\{' + key + '\\}', 'gm'), params[key]);
        }
        return `${ environment.apiUrl }/${ url }`;
    }
}
