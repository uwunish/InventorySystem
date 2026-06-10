import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { SaleService } from '../../../services/sale.service';
import { SaleDto } from '../../../models/sale.model';

@Component({
  selector: 'app-sale-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './sale-list.component.html',
  styleUrls: ['./sale-list.component.scss']
})
export class SaleListComponent implements OnInit {
  sales: SaleDto[] = [];
  displayedColumns = [
    'id', 'saleDate', 'customerName',
    'totalAmount', 'createdByName', 'actions'
  ];
  isLoading = false;

  constructor(
    private saleService: SaleService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.saleService.getAll().subscribe({
      next: (data) => {
        this.sales = data;
        this.isLoading = false;
      },
      error: () => this.isLoading = false
    });
  }

  viewDetail(id: number): void {
    this.router.navigate(['/sales', id]);
  }

  newSale(): void {
    this.router.navigate(['/sales/new']);
  }
}
