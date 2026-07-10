import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  loading = true;
  error: string | null = null;
  toastMessage: string | null = null;
  isToastVisible = false;
  private toastTimeoutId: number | null = null;

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  private loadProducts(): void {
    this.productService.getAllProducts().subscribe({
      next: (data) => {
        this.products = data;
        this.loading = false;
        console.log('Products loaded:', this.products);
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Failed to load products';
        this.loading = false;
        console.error('Error loading products:', err);
        this.cdr.detectChanges();
      },
    });
  }

  addToCart(product: Product): void {
    this.cartService.addItem(product.id, 1).subscribe({
      next: () => {
        this.showToast(`${product.name} added to cart.`);
      },
      error: (err) => {
        this.error = 'Unable to add product to cart. Please try again.';
        console.error('Error adding product to cart:', err);
        this.cdr.detectChanges();
      }
    });
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
}
