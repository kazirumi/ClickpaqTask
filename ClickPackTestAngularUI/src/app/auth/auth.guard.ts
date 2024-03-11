import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { nextTick } from 'process';
import { Observable } from 'rxjs';
import { AuthService } from '../shared/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  /**
   *
   */
  constructor(private router:Router,private service:AuthService) {
    

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot):  boolean  {
    
    if(localStorage.getItem('token')!=null){
      return true;
    }
    else{
      this.router.navigate(['/user/login']);
      return false;
    }
    

  }
  
}
