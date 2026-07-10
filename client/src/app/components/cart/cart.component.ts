import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Cart } from '../../models/cart.model';
import { CartItem } from '../../models/cart-item.model';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent implements OnInit {
  cart: Cart | null = null;
  loading = true;
  error: string | null = null;

  constructor(private cartService: CartService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.loadCart();
  }

  private loadCart(): void {
    this.cartService.getCart().subscribe({
      next: (data) => {
        this.cart = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Failed to load cart';
        this.loading = false;
        console.error('Error loading cart:', err);
        this.cdr.detectChanges();
      }
    });
  }

  updateQuantity(item: CartItem, change: number): void {
    const newQuantity = item.quantity + change;
    if (newQuantity < 1) {
      return;
    }

    this.cartService.updateItem(item.id, newQuantity).subscribe({
      next: (data) => {
        this.cart = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error updating cart item:', err);
      }
    });
  }

  removeItem(itemId: number): void {
    this.cartService.removeItem(itemId).subscribe({
      next: (data) => {
        this.cart = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error removing cart item:', err);
      }
    });
  }
}
