import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Cart } from '../models/cart.model';
import { CartItem } from '../models/cart-item.model';

interface AddCartItemRequest {
  productId: number;
  quantity: number;
}

interface UpdateCartItemRequest {
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5025/api/cart';
  private cartCountSubject = new BehaviorSubject(0);
  cartCount$ = this.cartCountSubject.asObservable();

  private cartTotalSubject = new BehaviorSubject(0);
  cartTotal$ = this.cartTotalSubject.asObservable();

  constructor(private http: HttpClient) {
    this.refreshCartState();
  }

  getCart(): Observable<Cart> {
    return this.http.get<Cart>(this.apiUrl).pipe(
      tap((cart) => {
        this.cartCountSubject.next(cart.totalItems);
        this.cartTotalSubject.next(cart.totalPrice);
      })
    );
  }

  addItem(productId: number, quantity: number): Observable<Cart> {
    const request: AddCartItemRequest = { productId, quantity };
    return this.http.post<Cart>(`${this.apiUrl}/items`, request).pipe(
      tap((cart) => {
        this.cartCountSubject.next(cart.totalItems);
        this.cartTotalSubject.next(cart.totalPrice);
      })
    );
  }

  updateItem(itemId: number, quantity: number): Observable<Cart> {
    const request: UpdateCartItemRequest = { quantity };
    return this.http.put<Cart>(`${this.apiUrl}/items/${itemId}`, request).pipe(
      tap((cart) => {
        this.cartCountSubject.next(cart.totalItems);
        this.cartTotalSubject.next(cart.totalPrice);
      })
    );
  }

  removeItem(itemId: number): Observable<Cart> {
    return this.http.delete<Cart>(`${this.apiUrl}/items/${itemId}`).pipe(
      tap((cart) => {
        this.cartCountSubject.next(cart.totalItems);
        this.cartTotalSubject.next(cart.totalPrice);
      })
    );
  }

  refreshCartState(): void {
    this.http.get<Cart>(this.apiUrl).subscribe({
      next: (cart) => {
        this.cartCountSubject.next(cart.totalItems);
        this.cartTotalSubject.next(cart.totalPrice);
      },
      error: () => {
        this.cartCountSubject.next(0);
        this.cartTotalSubject.next(0);
      }
    });
  }
}
