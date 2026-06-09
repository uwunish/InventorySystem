import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductGroup, CreateProductGroupRequest }
  from '../models/product-group.model';

@Injectable({ providedIn: 'root' })
export class ProductGroupService {
  private apiUrl = 'http://localhost:5000/api/productgroups';

  constructor(private http: HttpClient) { }

  getAll(): Observable<ProductGroup[]> {
    return this.http.get<ProductGroup[]>(this.apiUrl);
  }

  getById(id: number): Observable<ProductGroup> {
    return this.http.get<ProductGroup>(`${this.apiUrl}/${id}`);
  }

  create(data: CreateProductGroupRequest): Observable<ProductGroup> {
    return this.http.post<ProductGroup>(this.apiUrl, data);
  }

  update(id: number, data: CreateProductGroupRequest): Observable<ProductGroup> {
    return this.http.put<ProductGroup>(`${this.apiUrl}/${id}`, data);
  }
}
