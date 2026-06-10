export interface Customer {
  id: number;
  name: string;
  description: string;
  status: number;
}

export interface CreateCustomerRequest {
  name: string;
  description: string;
  status: number;
}
