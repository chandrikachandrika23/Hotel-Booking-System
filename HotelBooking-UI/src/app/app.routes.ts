import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { HotelListComponent } from './features/hotels/hotel-list/hotel-list';


export const routes: Routes = [
    {path:'',redirectTo:'login',pathMatch:'full'},
    {path:'register',component:Register},
    {path:'login',component:Login},
    {path:'hotels',component:HotelListComponent}
];
