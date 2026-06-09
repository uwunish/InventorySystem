import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { UnitOfMeasureService } from '../../../services/unit-of-measure.service';
import { UnitOfMeasure } from '../../../models/unit-of-measure.model';

@Component({
  selector: 'app-uom-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule,
    MatIconModule, MatChipsModule],
  templateUrl: './uom-list.component.html',
  styleUrls: ['./uom-list.component.scss']
})
export class UomListComponent implements OnInit {
  unitOfMeasures: UnitOfMeasure[] = [];
  displayedColumns = ['name', 'code', 'description', 'status', 'actions'];
  isLoading = false;

  constructor(
    private uomService: UnitOfMeasureService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;
    this.uomService.getAll().subscribe({
      next: (data) => {
        this.unitOfMeasures = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Failed to load units of measure', err);
        this.isLoading = false;
      }
    });
  }

  navigateToCreate(): void {
    this.router.navigate(['/unit-of-measures/new']);
  }

  navigateToEdit(id: number): void {
    this.router.navigate(['/unit-of-measures/edit', id]);
  }
}
