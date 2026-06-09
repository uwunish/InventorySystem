export interface UnitOfMeasure {
  id: number;
  name: string;
  code: string;
  description: string;
  status: number;
  createdAt: string;
}

export interface CreateUnitOfMeasureRequest {
  name: string;
  code: string;
  description: string;
  status: number;
}
