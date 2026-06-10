import { Component, OnInit } from '@angular/core';
import {
  FormBuilder, FormGroup, FormArray, Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { forkJoin } from 'rxjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatDividerModule } from '@angular/material/divider';
import { PurchaseService } from '../../../services/purchase.service';
import { VendorService } from '../../../services/vendor.service';
import { ProductService } from '../../../services/product.service';
import { Vendor } from '../../../models/vendor.model';
import { Product } from '../../../models/product.model';

@Component({
  selector: 'app-purchase-new',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, MatFormFieldModule,
    MatSelectModule, MatInputModule, MatButtonModule,
    MatCardModule, MatIconModule, MatTableModule, MatDividerModule
  ],
  templateUrl: './purchase-new.component.html',
  styleUrls: ['./purchase-new.component.scss']
})
export class PurchaseNewComponent implements OnInit {
  form: FormGroup;
  vendors: Vendor[] = [];
  products: Product[] = [];
  isLoadingDropdowns = true;
  isSubmitting = false;
  errorMessage = '';

  // Columns for the line items table
  itemColumns = [
    'product', 'quantity', 'purchasePrice', 'salesPrice', 'lineTotal', 'action'
  ];

  constructor(
    private fb: FormBuilder,
    private purchaseService: PurchaseService,
    private vendorService: VendorService,
    private productService: ProductService,
    private router: Router
  ) {
    this.form = this.fb.group({
      vendorId: [null, Validators.required],
      items: this.fb.array([], Validators.minLength(1))
    });
  }

  ngOnInit(): void {
    forkJoin({
      vendors: this.vendorService.getAll(),
      products: this.productService.getAll()
    }).subscribe({
      next: ({ vendors, products }) => {
        this.vendors = vendors.filter(v => v.status === 1);
        this.products = products.filter(p => p.status === 1);
        this.isLoadingDropdowns = false;
        // Start with one empty row
        this.addItem();
      },
      error: () => {
        this.errorMessage = 'Failed to load form data.';
        this.isLoadingDropdowns = false;
      }
    });
  }

  // Getter for easy access to the FormArray in the template
  get items(): FormArray {
    return this.form.get('items') as FormArray;
  }

  // Creates one line item FormGroup
  createItemGroup(): FormGroup {
    return this.fb.group({
      productId: [null, Validators.required],
      quantity: [1, [Validators.required, Validators.min(0.0001)]],
      purchasePrice: [0, [Validators.required, Validators.min(0.0001)]],
      salesPrice: [0, [Validators.required, Validators.min(0.0001)]]
    });
  }

  addItem(): void {
    this.items.push(this.createItemGroup());
  }

  removeItem(index: number): void {
    if (this.items.length > 1) {
      this.items.removeAt(index);
    }
  }

  // Computed line total for display
  getLineTotal(index: number): number {
    const item = this.items.at(index).value;
    return (item.quantity || 0) * (item.purchasePrice || 0);
  }

  // Grand total across all lines
  getGrandTotal(): number {
    return this.items.controls.reduce((sum, _, i) =>
      sum + this.getLineTotal(i), 0);
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.isSubmitting = true;
    this.errorMessage = '';

    this.purchaseService.create(this.form.value).subscribe({
      next: (purchase) => {
        this.router.navigate(['/purchases', purchase.id]);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || 'Failed to create purchase.';
        this.isSubmitting = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/purchases']);
  }
}
