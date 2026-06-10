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
import { VendorService } from '../../../services/vendor.service';

@Component({
  selector: 'app-vendor-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatSelectModule, MatButtonModule, MatCardModule],
  templateUrl: './vendor-form.component.html',
  styleUrls: ['./vendor-form.component.scss']
})
export class VendorFormComponent implements OnInit {
  form: FormGroup;
  isEditMode = false;
  editId?: number;
  errorMessage = '';
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private service: VendorService,
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
    const idParam = this.route.snapshot.params['id'];
    if (idParam) {
      this.isEditMode = true;
      this.editId = +idParam;
      this.loadExistingData();
    }
  }

  loadExistingData(): void {
    this.service.getById(this.editId!).subscribe({
      next: (data) => {
        this.form.patchValue({
          name: data.name,
          description: data.description,
          status: data.status
        });
      },
      error: () => this.router.navigate(['/vendors'])
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.isLoading = true;
    this.errorMessage = '';

    const operation = this.isEditMode
      ? this.service.update(this.editId!, this.form.value)
      : this.service.create(this.form.value);

    operation.subscribe({
      next: () => this.router.navigate(['/vendors']),
      error: (err) => {
        this.errorMessage = err.error?.message || 'An error occurred.';
        this.isLoading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/vendors']);
  }
}
