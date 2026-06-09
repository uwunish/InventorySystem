export interface Product {
  id: number;
  name: string;
  description: string;
  status: number;
  unitOfMeasureId: number;
  unitOfMeasureName: string;
  productGroupId: number;
  productGroupName: string;
  createdAt: string;
}

export interface CreateProductRequest {
  name: string;
  description: string;
  status: number;
  unitOfMeasureId: number;
  productGroupId: number;
}
