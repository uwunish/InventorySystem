import { Component, OnInit } from '@angular/core';
import {
  FormBuilder, FormGroup, FormArray,
  Validators, ReactiveFormsModule
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
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SaleService } from '../../../services/sale.service';
import { CustomerService } from '../../../services/customer.service';
import { ProductService } from '../../../services/product.service';
import { StockService } from '../../../services/stock.service';
import { Customer } from '../../../models/customer.model';
import { Product } from '../../../models/product.model';
import { StockItemDto } from '../../../models/stock.model';

@Component({
  selector: 'app-sale-new',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, MatFormFieldModule,
    MatSelectModule, MatInputModule, MatButtonModule,
    MatCardModule, MatIconModule, MatDividerModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './sale-new.component.html',
  styleUrls: ['./sale-new.component.scss']
})
export class SaleNewComponent implements OnInit {
  form: FormGroup;
  customers: Customer[] = [];
  products: Product[] = [];
  stockMap: Map<number, number> = new Map();
  isLoadingDropdowns = true;
  isSubmitting = false;
  errorMessage = '';

  itemColumns = ['product', 'available', 'quantity', 'salesPrice',
    'lineTotal', 'action'];

  constructor(
    private fb: FormBuilder,
    private saleService: SaleService,
    private customerService: CustomerService,
    private productService: ProductService,
    private stockService: StockService,
    private router: Router
  ) {
    this.form = this.fb.group({
      // null validator — customer is optional
      customerId: [null],
      items: this.fb.array([], Validators.minLength(1))
    });
  }

  ngOnInit(): void {
    forkJoin({
      customers: this.customerService.getAll(),
      products: this.productService.getAll(),
      stock: this.stockService.getCurrentStock()
    }).subscribe({
      next: ({ customers, products, stock }) => {
        this.customers = customers.filter(c => c.status === 1);
        this.products = products.filter(p => p.status === 1);

        // Build a quick-lookup map: productId → available stock quantity
        stock.forEach(s =>
          this.stockMap.set(s.productId, s.stockQuantity));

        this.isLoadingDropdowns = false;
        this.addItem();
      },
      error: () => {
        this.errorMessage = 'Failed to load form data.';
        this.isLoadingDropdowns = false;
      }
    });
  }

  get items(): FormArray {
    return this.form.get('items') as FormArray;
  }

  createItemGroup(): FormGroup {
    return this.fb.group({
      productId: [null, Validators.required],
      quantity: [1, [Validators.required, Validators.min(0.0001)]],
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

  // When a product is selected, pre-fill the sales price from stock data
  onProductSelected(index: number, productId: number): void {
    const product = this.products.find(p => p.id === productId);
    if (product) {
      // We don't have salesPrice on product directly —
      // the user sets the price. Start with 0 so they must enter it.
      // In a real system you'd store last sales price on the product.
      this.items.at(index).patchValue({ salesPrice: 0 });
    }
  }

  // Get available stock for a selected product in a row
  getAvailableStock(index: number): number {
    const productId = this.items.at(index).get('productId')?.value;
    return productId ? (this.stockMap.get(productId) ?? 0) : 0;
  }

  getLineTotal(index: number): number {
    const item = this.items.at(index).value;
    return (item.quantity || 0) * (item.salesPrice || 0);
  }

  getGrandTotal(): number {
    return this.items.controls.reduce(
      (sum, _, i) => sum + this.getLineTotal(i), 0);
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.isSubmitting = true;
    this.errorMessage = '';

    const payload = {
      customerId: this.form.value.customerId || null,
      items: this.form.value.items
    };

    this.saleService.create(payload).subscribe({
      next: (sale) => this.router.navigate(['/sales', sale.id]),
      error: (err) => {
        this.errorMessage = err.error?.message || 'Failed to create sale.';
        this.isSubmitting = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/sales']);
  }
}
