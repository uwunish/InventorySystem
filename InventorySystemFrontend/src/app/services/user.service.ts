import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface UserDto {
  id: number;
  name: string;
  email: string;
  mobileNo: string;
  status: number;
  createdAt: string;
}

export interface CreateUserRequest {
  name: string;
  email: string;
  mobileNo: string;
  password: string;
  status: number;
}

export interface UpdateUserRequest {
  name: string;
  mobileNo: string;
  status: number;
}

@Injectable({ providedIn: 'root' })
export class UserService {

  private apiUrl = `${environment.apiUrl}/users`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<UserDto> {
    return this.http.get<UserDto>(`${this.apiUrl}/${id}`);
  }

  create(data: CreateUserRequest): Observable<UserDto> {
    return this.http.post<UserDto>(this.apiUrl, data);
  }

  update(id: number, data: UpdateUserRequest): Observable<UserDto> {
    return this.http.put<UserDto>(`${this.apiUrl}/${id}`, data);
  }

  changePassword(id: number, newPassword: string): Observable<any> {
    return this.http.patch(`${this.apiUrl}/${id}/change-password`,
      { newPassword });
  }
}
