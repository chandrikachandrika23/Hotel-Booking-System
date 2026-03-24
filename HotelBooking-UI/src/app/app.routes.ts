import { Routes } from '@angular/router';
import { HotelsListComponent } from './hotels/hotels-list/hotels-list.component';

export const routes: Routes = [
  { path: '', redirectTo: 'hotels', pathMatch: 'full' },
  { path: 'hotels', component: HotelsListComponent },
];