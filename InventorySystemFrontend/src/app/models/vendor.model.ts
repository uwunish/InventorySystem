export interface Vendor {
  id: number;
  name: string;
  description: string;
  status: number;
}

export interface CreateVendorRequest {
  name: string;
  description: string;
  status: number;
}
