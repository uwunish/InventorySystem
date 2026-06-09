import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule }
  from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ProductGroupService }
  from '../../../services/product-group.service';

@Component({
  selector: 'app-product-group-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatSelectModule, MatButtonModule, MatCardModule],
  templateUrl: './product-group-form.component.html',
  styleUrls: ['./product-group-form.component.scss']
})
export class ProductGroupFormComponent implements OnInit {
  form: FormGroup;
  isEditMode = false;
  editId?: number;
  errorMessage = '';
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private service: ProductGroupService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', Validators.maxLength(500)],
      status: [1, Validators.required]
    });
  }

  ngOnInit(): void {
    this.editId = this.route.snapshot.params['id'];
    if (this.editId) {
      this.isEditMode = true;
      this.service.getById(this.editId).subscribe({
        next: (data) => this.form.patchValue(data),
        error: () => this.router.navigate(['/product-groups'])
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.isLoading = true;

    const operation = this.isEditMode
      ? this.service.update(this.editId!, this.form.value)
      : this.service.create(this.form.value);

    operation.subscribe({
      next: () => this.router.navigate(['/product-groups']),
      error: (err) => {
        this.errorMessage = err.error?.message || 'An error occurred.';
        this.isLoading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/product-groups']);
  }
}
