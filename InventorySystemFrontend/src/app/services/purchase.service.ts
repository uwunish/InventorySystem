import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PurchaseDto, CreatePurchaseRequest } from '../models/purchase.model';

@Injectable({ providedIn: 'root' })
export class PurchaseService {
  private apiUrl = 'http://localhost:5000/api/purchases';

  constructor(private http: HttpClient) { }

  getAll(): Observable<PurchaseDto[]> {
    return this.http.get<PurchaseDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<PurchaseDto> {
    return this.http.get<PurchaseDto>(`${this.apiUrl}/${id}`);
  }

  create(data: CreatePurchaseRequest): Observable<PurchaseDto> {
    return this.http.post<PurchaseDto>(this.apiUrl, data);
  }

}
