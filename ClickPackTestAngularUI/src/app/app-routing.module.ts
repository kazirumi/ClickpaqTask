import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { AuthGuard } from './auth/auth.guard';
import { ContactListComponent } from './contact-list/contact-list.component';

const routes: Routes = [
  {path:'dashboard', component:DashboardComponent,canActivate:[AuthGuard],data:{permittedRoles:["Admin","User"]}},
  // {path:'user-list',component:UserListComponent,canActivate:[AuthGuard],data:{permittedRoles:["Admin"]}},
  {path:'contact-list',component:ContactListComponent,canActivate:[AuthGuard]},
  {path:'user',component:UserComponent,children:[
    {path:'registration',component:RegistrationComponent},
    {path:'login',component:LoginComponent}

  ]},
  {path:'forbidden',component:ForbiddenComponent },
  {path:'',redirectTo:'/user/login',pathMatch:'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
