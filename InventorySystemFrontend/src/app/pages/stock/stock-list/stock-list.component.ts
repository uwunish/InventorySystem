import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { StockService } from '../../../services/stock.service';
import { StockItemDto } from '../../../models/stock.model';

@Component({
  selector: 'app-stock-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatCardModule,
    MatIconModule, MatButtonModule],
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.scss']
})
export class StockListComponent implements OnInit {
  stockItems: StockItemDto[] = [];
  displayedColumns = [
    'productName', 'productGroupName', 'unitOfMeasure',
    'totalPurchased', 'totalSold', 'stockQuantity',
    'averageRate', 'stockValue'
  ];
  isLoading = false;

  constructor(private stockService: StockService) { }

  ngOnInit(): void {
    this.loadStock();
  }

  loadStock(): void {
    this.isLoading = true;
    this.stockService.getCurrentStock().subscribe({
      next: (data) => {
        this.stockItems = data;
        this.isLoading = false;
      },
      error: () => this.isLoading = false
    });
  }

  get grandTotalValue(): number {
    return this.stockItems.reduce((sum, item) => sum + item.stockValue, 0);
  }
}
