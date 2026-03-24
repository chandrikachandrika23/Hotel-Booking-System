import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { HotelsListComponent } from './features/hotel-list.component/hotel-list.component';



export const routes: Routes = [
    {path:'',redirectTo:'login',pathMatch:'full'},
    {path:'register',component:Register},
    {path:'login',component:Login},
    {path:'hotels',component:HotelsListComponent}
];


