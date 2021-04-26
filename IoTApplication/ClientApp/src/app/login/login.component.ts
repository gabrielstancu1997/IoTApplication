import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  user = localStorage.getItem('user');
  constructor(
    public authService: AuthService,
    public router: Router,
    public http: HttpClient
    ) { }

  // tslint:disable-next-line: typedef
  Login(f: NgForm) {
    const loginObserver = {
      next: (x: any) => {
        alert('Welcome back ' + localStorage.getItem('user'));

        setTimeout(() => {
          this.router.navigate(['/home']);
      });
      },
      error: () => {
        alert('Fail');
      },
    };

    this.authService.login(f.value).subscribe(loginObserver);
  }

}
