import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vendor, CreateVendorRequest } from '../models/vendor.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class VendorService {

  private apiUrl = `${environment.apiUrl}/vendors`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<Vendor[]> {
    return this.http.get<Vendor[]>(this.apiUrl);
  }

  getById(id: number): Observable<Vendor> {
    return this.http.get<Vendor>(`${this.apiUrl}/${id}`);
  }

  create(data: CreateVendorRequest): Observable<Vendor> {
    return this.http.post<Vendor>(this.apiUrl, data);
  }

  update(id: number, data: CreateVendorRequest): Observable<Vendor> {
    return this.http.put<Vendor>(`${this.apiUrl}/${id}`, data);
  }
}
