import { Component, OnInit } from '@angular/core';
import { AuthService } from './shared/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Angular14RoleBasedAthurization';

  constructor(public service:AuthService){

  }
  ngOnInit(): void {
    if(localStorage.getItem('token')!=null){
      this.service.signInStatus=true;
    }
  }
}
