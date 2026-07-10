import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { OrderService } from '../../services/order.service';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent {
  customerName = '';
  customerEmail = '';
  shippingAddress = '';
  city = '';
  notes = '';
  loading = false;
  error: string | null = null;

  constructor(
    private orderService: OrderService,
    private cartService: CartService,
    private router: Router
  ) {}

  submitOrder(): void {
    if (!this.customerName || !this.customerEmail || !this.shippingAddress || !this.city) {
      this.error = 'Please fill in all required fields.';
      return;
    }

    this.loading = true;
    this.error = null;

    this.orderService.createOrder({
      customerName: this.customerName,
      customerEmail: this.customerEmail,
      shippingAddress: this.shippingAddress,
      city: this.city,
      notes: this.notes || undefined
    }).subscribe({
      next: (order) => {
        this.loading = false;
        this.cartService.refreshCartState();
        sessionStorage.setItem('orderSubmitted', 'true');
        this.router.navigate(['/confirmation', order.id], { state: { orderSubmitted: true } });
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Unable to submit order. Please try again.';
        console.error('Order submission failed:', err);
      }
    });
  }
}
