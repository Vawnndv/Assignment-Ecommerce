export interface Category {
  id: number;
  name: string;
  description?: string;
  parentCategoryId?: number | null;
  subCategories: Category[];
}
