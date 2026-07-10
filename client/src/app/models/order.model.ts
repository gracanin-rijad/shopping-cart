import { OrderItem } from './order-item.model';

export interface Order {
  id: number;
  customerName: string;
  customerEmail: string;
  shippingAddress: string;
  city: string;
  notes?: string;
  totalPrice: number;
  createdAt: string;
  items: OrderItem[];
}
