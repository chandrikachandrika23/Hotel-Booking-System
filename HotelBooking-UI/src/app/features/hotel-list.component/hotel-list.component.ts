import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HotelsService,Hotel } from '../../core/services/hotel';

@Component({
  selector: 'app-hotels-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './hotel-list.component.html',
  styleUrl: './hotel-list.component.css'
})
export class HotelsListComponent implements OnInit {

  hotels: Hotel[] = [];
  filteredHotels: Hotel[] = [];
  searchLocation: string = '';
  isLoading: boolean = false;
  errorMessage: string = '';

  constructor(private hotelsService: HotelsService) {}

  ngOnInit(): void {
    this.loadHotels();
  }

  loadHotels(): void {
    this.isLoading = true;
    this.hotelsService.getAllHotels().subscribe({
      next: (data:any) => {
  console.log("Hotels Data:", data);   // 🔥 DEBUG
  this.hotels = data;
  this.filteredHotels = data;
  this.isLoading = false;
},
      error: (err:any) => {
        this.errorMessage = 'Failed to load hotels. Please try again.';
        this.isLoading = false;
      }
    });
  }

  searchHotels(): void {
    if (this.searchLocation.trim() === '') {
      this.filteredHotels = this.hotels;
      return;
    }
    this.isLoading = true;
    this.hotelsService.searchHotels(this.searchLocation).subscribe({
      next: (data:any) => {
        this.filteredHotels = data;
        this.isLoading = false;
      },
      error: (err:any) => {
        this.errorMessage = 'Search failed. Please try again.';
        this.isLoading = false;
      }
    });
  }

  clearSearch(): void {
    this.searchLocation = '';
    this.filteredHotels = this.hotels;
  }
}