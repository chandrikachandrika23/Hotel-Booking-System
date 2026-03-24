import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  baseUrl = 'https://localhost:7164/api';

  constructor(private http: HttpClient) {}

  post(url: string, data: any) {
    return this.http.post(`${this.baseUrl}/${url}`, data);
  }

  get(url: string) {
    return this.http.get(`${this.baseUrl}/${url}`);
  }
 
}