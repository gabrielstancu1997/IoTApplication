import { Router } from '@angular/router';
import { AuthService } from '../service/auth.service';
import { Component } from '@angular/core';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  okToShowLogin = 'register';
  model: any = {
    username: null,
    password: null
  };

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }


  // tslint:disable-next-line: typedef
  onSubmit() {
    const registerObserver = {
      next: () => {
        this.okToShowLogin = 'login';
        alert('Account Created');

        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1000);
      },
      error: () => {
        alert('Fail');
      },
    };
    this.authService.register(this.model).subscribe(registerObserver);
  }

}
