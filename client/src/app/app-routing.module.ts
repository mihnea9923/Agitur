import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UserComponent } from './components/user/user.component';

const routes: Routes = [
  {
    path : '',
    redirectTo : '/user/login',
     pathMatch : 'full'
  },
  {
    path : 'user',
    component : UserComponent,
    children : [
      {
        path : 'registration',
        component : RegistrationComponent
      }
      ,
      {
        path : 'login',
        component : LoginComponent
      }
    ]
  },
  {
    path : 'home',
    component : HomeComponent,
    canActivate : [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
