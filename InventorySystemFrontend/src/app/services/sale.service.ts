import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SaleDto, CreateSaleRequest } from '../models/sale.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class SaleService {

  private apiUrl = `${environment.apiUrl}/sales`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<SaleDto[]> {
    return this.http.get<SaleDto[]>(this.apiUrl);
  }

  getById(id: number): Observable<SaleDto> {
    return this.http.get<SaleDto>(`${this.apiUrl}/${id}`);
  }

  create(data: CreateSaleRequest): Observable<SaleDto> {
    return this.http.post<SaleDto>(this.apiUrl, data);
  }
  // No update — immutable
}
