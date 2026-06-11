import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StockItemDto } from '../models/stock.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class StockService {
  
  private apiUrl = `${environment.apiUrl}/stock`;

  constructor(private http: HttpClient) { }

  getCurrentStock(): Observable<StockItemDto[]> {
    return this.http.get<StockItemDto[]>(this.apiUrl);
  }
}
