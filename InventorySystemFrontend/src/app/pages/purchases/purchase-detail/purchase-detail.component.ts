import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { PurchaseService } from '../../../services/purchase.service';
import { PurchaseDto } from '../../../models/purchase.model';

@Component({
  selector: 'app-purchase-detail',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatTableModule,
    MatButtonModule, MatIconModule, MatDividerModule],
  templateUrl: './purchase-detail.component.html',
  styleUrls: ['./purchase-detail.component.scss']
})
export class PurchaseDetailComponent implements OnInit {
  purchase?: PurchaseDto;
  displayedColumns = [
    'productName', 'quantity', 'purchasePrice', 'salesPrice', 'lineTotal'
  ];

  constructor(
    private route: ActivatedRoute,
    private purchaseService: PurchaseService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.params['id'];
    this.purchaseService.getById(id).subscribe({
      next: (data) => this.purchase = data,
      error: () => this.router.navigate(['/purchases'])
    });
  }

  goBack(): void {
    this.router.navigate(['/purchases']);
  }
}
