export interface PurchaseItemDto {
  id: number;
  productId: number;
  productName: string;
  quantity: number;
  purchasePrice: number;
  salesPrice: number;
  lineTotal: number;
}

export interface PurchaseDto {
  id: number;
  purchaseDate: string;
  vendorId: number;
  vendorName: string;
  createdByName: string;
  createdAt: string;
  items: PurchaseItemDto[];
  totalAmount: number;
}

export interface CreatePurchaseItemRequest {
  productId: number;
  quantity: number;
  purchasePrice: number;
  salesPrice: number;
}

export interface CreatePurchaseRequest {
  vendorId: number;
  items: CreatePurchaseItemRequest[];
}
