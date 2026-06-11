import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UnitOfMeasure, CreateUnitOfMeasureRequest }
  from '../models/unit-of-measure.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class UnitOfMeasureService {

  private apiUrl = `${environment.apiUrl}/unitofmeasures`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<UnitOfMeasure[]> {
    return this.http.get<UnitOfMeasure[]>(this.apiUrl);
  }

  getById(id: number): Observable<UnitOfMeasure> {
    return this.http.get<UnitOfMeasure>(`${this.apiUrl}/${id}`);
  }

  create(data: CreateUnitOfMeasureRequest): Observable<UnitOfMeasure> {
    return this.http.post<UnitOfMeasure>(this.apiUrl, data);
  }

  update(id: number, data: CreateUnitOfMeasureRequest): Observable<UnitOfMeasure> {
    return this.http.put<UnitOfMeasure>(`${this.apiUrl}/${id}`, data);
  }
}
