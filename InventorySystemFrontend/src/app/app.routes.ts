import { Routes } from '@angular/router';
import { authGuard, publicGuard } from './guards/auth.guard';

export const routes: Routes = [

  // Public routes — use auth layout (no navbar)
  {
    path: '',
    loadComponent: () =>
      import('./layout/auth-layout/auth-layout.component')
        .then(m => m.AuthLayoutComponent),
    children: [
      {
        path: 'login',
        canActivate: [publicGuard],
        loadComponent: () =>
          import('./pages/login/login.component')
            .then(m => m.LoginComponent)
      },
      {
        path: '',
        redirectTo: 'login',
        pathMatch: 'full'
      }
    ]
  },

  // Protected routes — use main layout (navbar always shown)
  {
    path: '',
    loadComponent: () =>
      import('./layout/main-layout/main-layout.component')
        .then(m => m.MainLayoutComponent),
    canActivate: [authGuard],
    children: [
      {
        path: 'product-groups',
        children: [
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
        path: 'unit-of-measures',
        children: [
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
        path: 'products',
        children: [
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
        path: 'vendors',
        children: [
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
      {
        path: 'customers',
        children: [
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
      {
        path: 'purchases',
        children: [
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
        path: 'sales',
        children: [
          {
            path: '', loadComponent: () =>
              import('./pages/sales/sale-list/sale-list.component')
                .then(m => m.SaleListComponent)
          },
          {
            path: 'new', loadComponent: () =>
              import('./pages/sales/sale-new/sale-new.component')
                .then(m => m.SaleNewComponent)
          },
          {
            path: ':id', loadComponent: () =>
              import('./pages/sales/sale-detail/sale-detail.component')
                .then(m => m.SaleDetailComponent)
          }
        ]
      },
      {
        path: 'stock',
        loadComponent: () =>
          import('./pages/stock/stock-list/stock-list.component')
            .then(m => m.StockListComponent)
      },
      {
        path: 'users',
        children: [
          {
            path: '', loadComponent: () =>
              import('./pages/users/user-list/user-list.component')
                .then(m => m.UserListComponent)
          },
          {
            path: 'new', loadComponent: () =>
              import('./pages/users/user-form/user-form.component')
                .then(m => m.UserFormComponent)
          },
          {
            path: 'edit/:id', loadComponent: () =>
              import('./pages/users/user-form/user-form.component')
                .then(m => m.UserFormComponent)
          }
        ]
      },
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./pages/dashboard/dashboard.component')
            .then(m => m.DashboardComponent)
      },
      // Default authenticated route
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

      {
        path: '**', loadComponent: () =>
          import('./pages/not-found/not-found.component')
            .then(m => m.NotFoundComponent)
      }
    ]
  }
];

