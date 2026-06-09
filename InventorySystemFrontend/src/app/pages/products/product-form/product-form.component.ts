import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule }
  from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProductService } from '../../../services/product.service';
import { ProductGroupService }
  from '../../../services/product-group.service';
import { UnitOfMeasureService }
  from '../../../services/unit-of-measure.service';
import { ProductGroup } from '../../../models/product-group.model';
import { UnitOfMeasure } from '../../../models/unit-of-measure.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatSelectModule, MatButtonModule,
    MatCardModule, MatProgressSpinnerModule
  ],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  form: FormGroup;
  isEditMode = false;
  editId?: number;
  errorMessage = '';
  isLoading = false;
  isLoadingDropdowns = true; // separate flag for dropdown loading

  // Dropdown options
  productGroups: ProductGroup[] = [];
  unitOfMeasures: UnitOfMeasure[] = [];

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private productGroupService: ProductGroupService,
    private uomService: UnitOfMeasureService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', Validators.maxLength(500)],
      productGroupId: [null, Validators.required],
      unitOfMeasureId: [null, Validators.required],
      status: [1, Validators.required]
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.params['id'];
    if (idParam) {
      this.isEditMode = true;
      this.editId = +idParam;
    }

    this.loadDropdowns();
  }

  loadDropdowns(): void {
    // forkJoin runs both API calls simultaneously and waits for BOTH
    // to complete before continuing — much faster than sequential calls
    forkJoin({
      groups: this.productGroupService.getAll(),
      uoms: this.uomService.getAll()
    }).subscribe({
      next: ({ groups, uoms }) => {
        // Only show Active items in dropdowns
        this.productGroups = groups.filter(g => g.status === 1);
        this.unitOfMeasures = uoms.filter(u => u.status === 1);
        this.isLoadingDropdowns = false;

        // Load existing product AFTER dropdowns are ready
        // so that patchValue can find the matching dropdown option
        if (this.isEditMode) {
          this.loadExistingData();
        }
      },
      error: () => {
        this.errorMessage = 'Failed to load dropdown data.';
        this.isLoadingDropdowns = false;
      }
    });
  }

  loadExistingData(): void {
    this.productService.getById(this.editId!).subscribe({
      next: (data) => {
        this.form.patchValue({
          name: data.name,
          description: data.description,
          productGroupId: data.productGroupId,
          unitOfMeasureId: data.unitOfMeasureId,
          status: data.status
        });
      },
      error: () => this.router.navigate(['/products'])
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.isLoading = true;
    this.errorMessage = '';

    const operation = this.isEditMode
      ? this.productService.update(this.editId!, this.form.value)
      : this.productService.create(this.form.value);

    operation.subscribe({
      next: () => this.router.navigate(['/products']),
      error: (err) => {
        this.errorMessage = err.error?.message || 'An error occurred.';
        this.isLoading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/products']);
  }
}
