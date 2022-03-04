/* eslint-disable @typescript-eslint/explicit-function-return-type */
import { Inject, Injectable } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { environment } from 'environments/environment';


@Injectable({
    providedIn: 'root'
})
export class ApiInterceptor implements HttpInterceptor {

    /**
     * Intercept
     *
     * @param request
     * @param next
     */
    intercept(request: HttpRequest<any>, next: HttpHandler) {

        const newReq = request.clone();
        if (!request.url.startsWith('https://viacep.com.br/ws')) {
            newReq.headers.append('x-functions-key', environment.functionKey);
        }

        return next.handle(newReq);
    }
}
