export interface StockItemDto {
  productId: number;
  productName: string;
  productGroupName: string;
  unitOfMeasureName: string;
  unitOfMeasureCode: string;
  totalPurchased: number;
  totalSold: number;
  stockQuantity: number;
  averageRate: number;
  stockValue: number;
}
