import { Component, OnInit } from '@angular/core';
import {
  FormBuilder, FormGroup, Validators,
  ReactiveFormsModule
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatSelectModule, MatButtonModule,
    MatCardModule, MatDividerModule],
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss']
})
export class UserFormComponent implements OnInit {
  profileForm: FormGroup;
  passwordForm: FormGroup;
  isEditMode = false;
  editId?: number;
  errorMessage = '';
  passwordError = '';
  passwordSuccess = '';
  isLoading = false;
  isChangingPassword = false;

  constructor(
    private fb: FormBuilder,
    private service: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.profileForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email]],
      mobileNo: ['', Validators.required],
      password: [''],
      status: [1, Validators.required]
    });

    this.passwordForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.params['id'];
    if (idParam) {
      this.isEditMode = true;
      this.editId = +idParam;

      // In edit mode, password is not required
      this.profileForm.get('email')?.disable();
      this.loadExistingData();
    } else {
      // In create mode, password is required
      this.profileForm.get('password')
        ?.setValidators([Validators.required, Validators.minLength(6)]);
      this.profileForm.get('password')?.updateValueAndValidity();
    }
  }

  loadExistingData(): void {
    this.service.getById(this.editId!).subscribe({
      next: (data) => {
        this.profileForm.patchValue({
          name: data.name,
          email: data.email,
          mobileNo: data.mobileNo,
          status: data.status
        });
      },
      error: () => this.router.navigate(['/users'])
    });
  }

  onSubmit(): void {
    if (this.profileForm.invalid) return;
    this.isLoading = true;
    this.errorMessage = '';

    if (this.isEditMode) {
      const updateData = {
        name: this.profileForm.value.name,
        mobileNo: this.profileForm.value.mobileNo,
        status: this.profileForm.value.status
      };
      this.service.update(this.editId!, updateData).subscribe({
        next: () => this.router.navigate(['/users']),
        error: (err) => {
          this.errorMessage = err.error?.message || 'An error occurred.';
          this.isLoading = false;
        }
      });
    } else {
      this.service.create(this.profileForm.value).subscribe({
        next: () => this.router.navigate(['/users']),
        error: (err) => {
          this.errorMessage = err.error?.message || 'An error occurred.';
          this.isLoading = false;
        }
      });
    }
  }

  onChangePassword(): void {
    if (this.passwordForm.invalid) return;

    const { newPassword, confirmPassword } = this.passwordForm.value;
    if (newPassword !== confirmPassword) {
      this.passwordError = 'Passwords do not match.';
      return;
    }

    this.isChangingPassword = true;
    this.passwordError = '';
    this.passwordSuccess = '';

    this.service.changePassword(this.editId!, newPassword).subscribe({
      next: () => {
        this.passwordSuccess = 'Password changed successfully.';
        this.passwordForm.reset();
        this.isChangingPassword = false;
      },
      error: (err) => {
        this.passwordError = err.error?.message || 'Failed to change password.';
        this.isChangingPassword = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/users']);
  }
}
