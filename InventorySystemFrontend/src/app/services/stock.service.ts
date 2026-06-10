import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StockItemDto } from '../models/stock.model';

@Injectable({ providedIn: 'root' })
export class StockService {
  private apiUrl = 'http://localhost:5000/api/stock';

  constructor(private http: HttpClient) { }

  getCurrentStock(): Observable<StockItemDto[]> {
    return this.http.get<StockItemDto[]>(this.apiUrl);
  }
}
