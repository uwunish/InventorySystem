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
  // Vendors
  {
    path: 'vendors', canActivate: [authGuard], children: [
      {
        path: '', loadComponent: () =>
          import('./pages/vendors/vendor-list/vendor-list.component')
            .then(m => m.VendorListComponent)
      },
      {
        path: 'new', loadComponent: () =>
          import('./pages/vendors/vendor-form/vendor-form.component')
            .then(m => m.VendorFormComponent)
      },
      {
        path: 'edit/:id', loadComponent: () =>
          import('./pages/vendors/vendor-form/vendor-form.component')
            .then(m => m.VendorFormComponent)
      }
    ]
  },

  // Customers
  {
    path: 'customers', canActivate: [authGuard], children: [
      {
        path: '', loadComponent: () =>
          import('./pages/customers/customer-list/customer-list.component')
            .then(m => m.CustomerListComponent)
      },
      {
        path: 'new', loadComponent: () =>
          import('./pages/customers/customer-form/customer-form.component')
            .then(m => m.CustomerFormComponent)
      },
      {
        path: 'edit/:id', loadComponent: () =>
          import('./pages/customers/customer-form/customer-form.component')
            .then(m => m.CustomerFormComponent)
      }
    ]
  },

  // Purchases
  {
    path: 'purchases', canActivate: [authGuard], children: [
      {
        path: '', loadComponent: () =>
          import('./pages/purchases/purchase-list/purchase-list.component')
            .then(m => m.PurchaseListComponent)
      },
      {
        path: 'new', loadComponent: () =>
          import('./pages/purchases/purchase-new/purchase-new.component')
            .then(m => m.PurchaseNewComponent)
      },
      {
        path: ':id', loadComponent: () =>
          import('./pages/purchases/purchase-detail/purchase-detail.component')
            .then(m => m.PurchaseDetailComponent)
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
