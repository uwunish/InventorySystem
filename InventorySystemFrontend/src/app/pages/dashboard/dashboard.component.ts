import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { forkJoin } from 'rxjs';
import { ProductService } from '../../services/product.service';
import { PurchaseService } from '../../services/purchase.service';
import { SaleService } from '../../services/sale.service';
import { StockService } from '../../services/stock.service';
import { AuthService } from '../../services/auth.service';
import { inject } from '@angular/core/primitives/di';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatIconModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  stats = {
    products: 0,
    purchases: 0,
    sales: 0,
    stockValue: 0
  };
  private authService = inject(AuthService);
  private productService = inject(ProductService);
  private purchaseService = inject(PurchaseService);
  private saleService = inject(SaleService);
  private stockService = inject(StockService);
  private router = inject(Router);

  currentUser = this.authService.getCurrentUser();
  isLoading = true;

  ngOnInit(): void {
    forkJoin({
      products: this.productService.getAll(),
      purchases: this.purchaseService.getAll(),
      sales: this.saleService.getAll(),
      stock: this.stockService.getCurrentStock()
    }).subscribe({
      next: ({ products, purchases, sales, stock }) => {
        this.stats.products = products.length;
        this.stats.purchases = purchases.length;
        this.stats.sales = sales.length;
        this.stats.stockValue = stock.reduce(
          (sum, item) => sum + item.stockValue, 0);
        this.isLoading = false;
      },
      error: () => this.isLoading = false
    });
  }

  navigate(path: string): void {
    this.router.navigate([path]);
  }
}
