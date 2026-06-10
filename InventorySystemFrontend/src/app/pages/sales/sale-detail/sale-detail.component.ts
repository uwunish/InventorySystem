import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatChipsModule } from '@angular/material/chips';
import { SaleService } from '../../../services/sale.service';
import { SaleDto } from '../../../models/sale.model';

@Component({
  selector: 'app-sale-detail',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatTableModule,
    MatButtonModule, MatIconModule, MatDividerModule,
    MatChipsModule],
  templateUrl: './sale-detail.component.html',
  styleUrls: ['./sale-detail.component.scss']
})
export class SaleDetailComponent implements OnInit {
  sale?: SaleDto;
  displayedColumns = [
    'productName', 'quantity', 'salesPrice', 'lineTotal'
  ];

  constructor(
    private route: ActivatedRoute,
    private saleService: SaleService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.params['id'];
    this.saleService.getById(id).subscribe({
      next: (data) => this.sale = data,
      error: () => this.router.navigate(['/sales'])
    });
  }

  goBack(): void {
    this.router.navigate(['/sales']);
  }
}
