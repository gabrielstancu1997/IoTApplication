import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  appUrl: string ;
  helper = new JwtHelperService();
  constructor( private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
      this.appUrl = baseUrl;
    }

  // tslint:disable-next-line: typedef
  login(model: any){ // : Observable<IUser>
    return this.http.post(this.appUrl + 'api/Auth/login', model).pipe(
      map((response: any) => {
        const decodedToken = this.helper.decodeToken(response.message);
        localStorage.setItem('user', response.username);
        localStorage.setItem('message', response.token);

      })
    );
  }

  loggedIn(): boolean{
    const message = String(localStorage.getItem('message'));
    return !this.helper.isTokenExpired(message);
  }

  // tslint:disable-next-line: typedef
  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('message');
  }
  // tslint:disable-next-line: typedef
  register(model: any){
    return  this.http.post(this.appUrl + 'api/Auth/register', model);
  }
}
