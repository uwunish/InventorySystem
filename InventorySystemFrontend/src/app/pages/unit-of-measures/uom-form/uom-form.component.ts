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
import { UnitOfMeasureService }
  from '../../../services/unit-of-measure.service';

@Component({
  selector: 'app-uom-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatSelectModule, MatButtonModule, MatCardModule],
  templateUrl: './uom-form.component.html',
  styleUrls: ['./uom-form.component.scss']
})
export class UomFormComponent implements OnInit {
  form: FormGroup;
  isEditMode = false;
  editId?: number;
  errorMessage = '';
  isLoading = false;

  constructor(
    private fb: FormBuilder,
    private service: UnitOfMeasureService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      code: ['', [Validators.required, Validators.maxLength(20)]],
      description: ['', Validators.maxLength(500)],
      status: [1, Validators.required]
    });
  }

  ngOnInit(): void {
    // Read the :id param from the URL — if it exists, we are in edit mode
    const idParam = this.route.snapshot.params['id'];
    if (idParam) {
      this.isEditMode = true;
      this.editId = +idParam; // The + converts string to number
      this.loadExistingData();
    }
  }

  loadExistingData(): void {
    this.service.getById(this.editId!).subscribe({
      next: (data) => {
        // patchValue fills only the fields that exist — safe even if API
        // returns extra fields your form does not have
        this.form.patchValue({
          name: data.name,
          code: data.code,
          description: data.description,
          status: data.status
        });
      },
      error: () => this.router.navigate(['/unit-of-measures'])
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
      next: () => this.router.navigate(['/unit-of-measures']),
      error: (err) => {
        this.errorMessage = err.error?.message || 'An error occurred.';
        this.isLoading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/unit-of-measures']);
  }
}
