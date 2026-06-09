export interface ProductGroup {
  id: number;
  name: string;
  description: string;
  status: number;
  userId: number;
  createdAt: string;
}

export interface CreateProductGroupRequest {
  name: string;
  description: string;
  status: number;
}
