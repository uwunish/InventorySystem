import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [MatButtonModule, MatIconModule],
  template: `
    <div class="not-found-container">
      <mat-icon class="not-found-icon">search_off</mat-icon>
      <h1>404</h1>
      <p>The page you are looking for does not exist.</p>
      <button mat-raised-button color="primary" (click)="goHome()">
        Go to Dashboard
      </button>
    </div>
  `,
  styles: [`
    .not-found-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      min-height: 60vh;
      gap: 16px;
      color: #666;
    }
    .not-found-icon {
      font-size: 64px;
      width: 64px;
      height: 64px;
      color: #ccc;
    }
    h1 { font-size: 72px; margin: 0; color: #333; }
    p  { font-size: 18px; margin: 0; }
  `]
})
export class NotFoundComponent {
  constructor(private router: Router) { }
  goHome(): void { this.router.navigate(['/']); }
}
