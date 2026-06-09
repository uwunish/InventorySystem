import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { ProductGroupService } from '../../../services/product-group.service';
import { ProductGroup } from '../../../models/product-group.model';

@Component({
  selector: 'app-product-group-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule,
    MatIconModule, MatChipsModule],
  templateUrl: './product-group-list.component.html',
  styleUrls: ['./product-group-list.component.scss']
})
export class ProductGroupListComponent implements OnInit {
  productGroups: ProductGroup[] = [];
  displayedColumns = ['name', 'description', 'status', 'actions'];

  constructor(
    private productGroupService: ProductGroupService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadProductGroups();
  }

  loadProductGroups(): void {
    this.productGroupService.getAll().subscribe({
      next: (data) => this.productGroups = data,
      error: (err) => console.error('Failed to load product groups', err)
    });
  }

  navigateToCreate(): void {
    this.router.navigate(['/product-groups/new']);
  }

  navigateToEdit(id: number): void {
    this.router.navigate(['/product-groups/edit', id]);
  }
}
