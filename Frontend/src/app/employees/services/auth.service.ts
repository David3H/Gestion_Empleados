import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

interface LoginResponse { token: string; }

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);
  private readonly url = 'http://localhost:5221/api/Auth';

  login(username: string, password: string) {
    return this.http.post<LoginResponse>(`${this.url}/login`, { username, password })
      .pipe(tap(res => localStorage.setItem('jwt_token', res.token)));
  }

  getToken(): string | null {
    return localStorage.getItem('jwt_token');
  }

  logout() {
    localStorage.removeItem('jwt_token');
  }

  isLoggedIn() {
    return !!this.getToken();
  }
}
