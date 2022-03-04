import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class CepService {
    result: Cep;
    constructor(private http: HttpClient) { }

    search(cep: string): Observable<Cep> {
        return this.http
            .get(`https://viacep.com.br/ws/${cep}/json/`)
            .pipe(
                map(data => this.result = this.cepConverter(data))
            );
    }

    private cepConverter(cepResponse): Cep {
        return {
            cep: cepResponse.cep,
            street: cepResponse.logradouro,
            complement: cepResponse.complemento,
            neighbourhood: cepResponse.bairro,
            city: cepResponse.localidade,
            state: cepResponse.uf,
        };
    }
}

export interface Cep {
    cep: string;
    street: string;
    complement: string;
    neighbourhood: string;
    city: string;
    state: string;
}
