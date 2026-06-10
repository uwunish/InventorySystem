import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { VendorService } from '../../../services/vendor.service';
import { Vendor } from '../../../models/vendor.model';

@Component({
  selector: 'app-vendor-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule,
    MatIconModule, MatChipsModule, MatTooltipModule],
  templateUrl: './vendor-list.component.html',
  styleUrls: ['./vendor-list.component.scss']
})
export class VendorListComponent implements OnInit {
  vendors: Vendor[] = [];
  displayedColumns = ['name', 'description', 'status', 'actions'];
  isLoading = false;

  constructor(
    private vendorService: VendorService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;
    this.vendorService.getAll().subscribe({
      next: (data) => {
        this.vendors = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Failed to load vendors', err);
        this.isLoading = false;
      }
    });
  }

  navigateToCreate(): void {
    this.router.navigate(['/vendors/new']);
  }

  navigateToEdit(id: number): void {
    this.router.navigate(['/vendors/edit', id]);
  }
}
