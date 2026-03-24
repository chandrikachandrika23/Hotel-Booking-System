import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Hotel {
  hotelId: number;
  name: string;
  location: string;
  amenities: string;
}

export interface CreateHotel {
  name: string;
  location: string;
  amenities: string;
}

@Injectable({
  providedIn: 'root'
})
export class HotelsService {

  private apiUrl = 'https://localhost:5062/api/Hotels'; 

  constructor(private http: HttpClient) {}

  getAllHotels(): Observable<Hotel[]> {
    return this.http.get<Hotel[]>(this.apiUrl);
  }

  getHotelById(id: number): Observable<Hotel> {
    return this.http.get<Hotel>(`${this.apiUrl}/${id}`);
  }

  searchHotels(location: string): Observable<Hotel[]> {
    return this.http.get<Hotel[]>(`${this.apiUrl}/search?location=${location}`);
  }

  createHotel(hotel: CreateHotel): Observable<Hotel> {
    return this.http.post<Hotel>(this.apiUrl, hotel);
  }

  updateHotel(id: number, hotel: CreateHotel): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, hotel);
  }

  deleteHotel(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}