import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, tap } from "rxjs";
import { environment } from "../../environments/environment";

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  name: string;
  email: string;
  userId: number;
}

@Injectable({ providedIn: "root" })
export class AuthService {
  private apiUrl = `${environment.apiUrl}`

  constructor(private http: HttpClient, private router: Router) { }

  login(credentials: LoginRequest): Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${this.apiUrl}/auth/login`, credentials)
      .pipe(
        tap(response => {
          localStorage.setItem("token", response.token);
          localStorage.setItem("user", JSON.stringify({
            name: response.name,
            email: response.email,
            userId: response.userId
          }));
        })
      );
  }

  logout(): void {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    this.router.navigate(["/login"]);
  }

  isLoggedIn(): boolean {
    return localStorage.getItem("token") !== null;
  }

  getToken(): string | null {
    return localStorage.getItem("token");
  }

  getCurrentUser() {
    const user = localStorage.getItem("user");
    return user ? JSON.parse(user) : null;
  }

}
