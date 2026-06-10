export interface SaleItemDto {
  id: number;
  productId: number;
  productName: string;
  quantity: number;
  salesPrice: number;
  lineTotal: number;
}

export interface SaleDto {
  id: number;
  saleDate: string;
  customerId: number | null;
  customerName: string;
  createdByName: string;
  createdAt: string;
  items: SaleItemDto[];
  totalAmount: number;
}

export interface CreateSaleItemRequest {
  productId: number;
  quantity: number;
  salesPrice: number;
}

export interface CreateSaleRequest {
  customerId: number | null;
  items: CreateSaleItemRequest[];
}
