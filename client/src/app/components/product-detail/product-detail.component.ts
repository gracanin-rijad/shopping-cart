import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {
  product: Product | null = null;
  loading = true;
  error: string | null = null;
  quantity = 1;
  toastMessage: string | null = null;
  isToastVisible = false;
  private toastTimeoutId: number | null = null;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      this.loadProduct(id);
    });
  }

  private loadProduct(id: number): void {
    this.productService.getProductById(id).subscribe({
      next: (data) => {
        this.product = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Product not found';
        this.loading = false;
        console.error('Error loading product:', err);
        this.cdr.detectChanges();
      }
    });
  }

  incrementQuantity(): void {
    if (this.product && this.quantity < this.product.stockQuantity) {
      this.quantity++;
    }
  }

  decrementQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  private showToast(message: string): void {
    this.toastMessage = message;
    this.isToastVisible = true;
    this.cdr.detectChanges();

    if (this.toastTimeoutId !== null) {
      window.clearTimeout(this.toastTimeoutId);
    }

    this.toastTimeoutId = window.setTimeout(() => {
      this.isToastVisible = false;
      this.toastMessage = null;
      this.cdr.detectChanges();
    }, 3000);
  }

  addToCart(): void {
    if (!this.product) {
      return;
    }

    this.cartService.addItem(this.product.id, this.quantity).subscribe({
      next: () => {
        this.showToast(`${this.quantity} item${this.quantity > 1 ? 's' : ''} added to cart.`);
      },
      error: (err) => {
        this.error = 'Unable to add item to cart. Please try again.';
        console.error('Error adding item to cart:', err);
        this.cdr.detectChanges();
      }
    });
  }
}
