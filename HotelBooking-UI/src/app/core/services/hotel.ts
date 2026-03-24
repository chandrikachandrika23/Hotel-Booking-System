import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HotelService {

  private baseUrl = 'https://localhost:7164/api/hotels';

  constructor(private http: HttpClient) {}

  // 🔹 Get all hotels
  getHotels() {
    return this.http.get(`${this.baseUrl}/Hotels`);
  }

  // 🔹 Get hotel by id (optional)
  getHotelById(id: number) {
    return this.http.get(`${this.baseUrl}/${id}`);
  }
}