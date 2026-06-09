import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './layout/navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavbarComponent, CommonModule],
  // templateUrl: './app.html',
  // styleUrl: './app.scss'
  template: `
    <app-navbar *ngIf="authService.isLoggedIn()"></app-navbar>
    <router-outlet></router-outlet>
  `
})
export class App {
  // protected readonly title = signal('InventorySystemFrontend');
  constructor(public authService: AuthService) { }
}
