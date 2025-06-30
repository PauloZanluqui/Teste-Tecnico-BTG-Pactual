import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ApiService {
    private baseUrl = 'http://localhost:5201';

    constructor(private http: HttpClient) { }

    async get<T>(endpoint: string): Promise<T> {
        return await firstValueFrom(this.http.get<T>(`${this.baseUrl}${endpoint}`));
    }

    async post<T>(endpoint: string, body: any): Promise<T> {
        return await firstValueFrom(this.http.post<T>(`${this.baseUrl}${endpoint}`, body));
    }

    async patch<T>(endpoint: string, body: any): Promise<T> {
        return await firstValueFrom(this.http.patch<T>(`${this.baseUrl}${endpoint}`, body));
    }

    async delete<T>(endpoint: string): Promise<T> {
        return await firstValueFrom(this.http.delete<T>(`${this.baseUrl}${endpoint}`));
    }
}
