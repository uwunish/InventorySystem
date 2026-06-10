import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { PurchaseService } from '../../../services/purchase.service';
import { PurchaseDto } from '../../../models/purchase.model';

@Component({
  selector: 'app-purchase-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './purchase-list.component.html',
  styleUrls: ['./purchase-list.component.scss']
})
export class PurchaseListComponent implements OnInit {
  purchases: PurchaseDto[] = [];
  displayedColumns = [
    'id', 'purchaseDate', 'vendorName',
    'totalAmount', 'createdByName', 'actions'
  ];
  isLoading = false;

  constructor(
    private purchaseService: PurchaseService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.purchaseService.getAll().subscribe({
      next: (data) => {
        this.purchases = data;
        this.isLoading = false;
      },
      error: () => this.isLoading = false
    });
  }

  viewDetail(id: number): void {
    this.router.navigate(['/purchases', id]);
  }

  newPurchase(): void {
    this.router.navigate(['/purchases/new']);
  }
}
