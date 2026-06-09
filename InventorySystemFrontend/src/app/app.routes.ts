import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login', loadComponent: () =>
      import('./pages/login/login.component')
        .then(m => m.LoginComponent)
  },
  {
    path: 'product-groups', canActivate: [authGuard], children: [
      {
        path: '', loadComponent: () =>
          import('./pages/product-groups/product-group-list/product-group-list.component')
            .then(m => m.ProductGroupListComponent)
      },
      {
        path: 'new', loadComponent: () =>
          import('./pages/product-groups/product-group-form/product-group-form.component')
            .then(m => m.ProductGroupFormComponent)
      },
      {
        path: 'edit/:id', loadComponent: () =>
          import('./pages/product-groups/product-group-form/product-group-form.component')
            .then(m => m.ProductGroupFormComponent)
      }
    ]
  },
  {
    path: 'unit-of-measures', canActivate: [authGuard], children: [
      {
        path: '', loadComponent: () =>
          import('./pages/unit-of-measures/uom-list/uom-list.component')
            .then(m => m.UomListComponent)
      },
      {
        path: 'new', loadComponent: () =>
          import('./pages/unit-of-measures/uom-form/uom-form.component')
            .then(m => m.UomFormComponent)
      },
      {
        path: 'edit/:id', loadComponent: () =>
          import('./pages/unit-of-measures/uom-form/uom-form.component')
            .then(m => m.UomFormComponent)
      }
    ]
  },
  {
    path: 'products', canActivate: [authGuard], children: [
      {
        path: '', loadComponent: () =>
          import('./pages/products/product-list/product-list.component')
            .then(m => m.ProductListComponent)
      },
      {
        path: 'new', loadComponent: () =>
          import('./pages/products/product-form/product-form.component')
            .then(m => m.ProductFormComponent)
      },
      {
        path: 'edit/:id', loadComponent: () =>
          import('./pages/products/product-form/product-form.component')
            .then(m => m.ProductFormComponent)
      }
    ]
  },
  {
    path: 'users', loadComponent: () =>
      import('./pages/users/user-list/user-list.component')
        .then(m => m.UserListComponent),
    canActivate: [authGuard]
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' }
];
