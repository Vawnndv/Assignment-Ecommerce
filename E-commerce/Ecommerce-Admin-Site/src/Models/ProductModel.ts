// ProductType interface
interface ProductType {
  id: number;
  type: string;
  description: string;
  productId: number;
  productImages: ProductImage[];
}

// ProductImage interface
interface ProductImage {
  id: number;
  imageUrl: string;
  productTypeId: number;
}

// ProductRating interface
interface ProductRating {
  id: number;
  rating: number;
  review: string;
  productId: number;
}

// Product interface
interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  discount: number;
  categoryId: number;
  productTypes: ProductType[];
  ratings: ProductRating[];
}

interface ProductQuery {
  Search?: string | null;
  IsLatest?: boolean;
  IsDiscount?: boolean;
  SortBy?: string | null;
  IsDescending?: boolean;
  PageNumber?: number;
  PageSize?: number;
  PageLimit?: number;
  MaxPrice?: number;
  MinPrice?: number;
}


// Function to convert a Product object to a JSON string
const convertProductToJson = (product: Product): string => {
  const productJson = {
    id: product.id,
    name: product.name,
    description: product.description,
    price: product.price,
    discount: product.discount,
    categoryId: product.categoryId,
    productTypes: product.productTypes.map(type => ({
      id: type.id,
      type: type.type,
      description: type.description,
      productId: type.productId,
      productImages: type.productImages.map(image => ({
        id: image.id,
        imageUrl: image.imageUrl,
        productTypeId: image.productTypeId,
      })),
    })),
    ratings: product.ratings.map(rating => ({
      id: rating.id,
      rating: rating.rating,
      review: rating.review,
      productId: rating.productId,
    })),
  };

  return JSON.stringify(productJson);
};

// Function to convert a JSON string to a Product object
const convertJsonToProduct = (data: any): Product => {
  return {
    id: data.id,
    name: data.name,
    description: data.description,
    price: data.price,
    discount: data.discount,
    categoryId: data.categoryId,
    productTypes: data.productTypes.map((type: any) => ({
      id: type.id,
      type: type.type,
      description: type.description,
      productId: type.productId,
      productImages: type.productImages.map((image: any) => ({
        id: image.id,
        imageUrl: image.imageUrl,
        productTypeId: image.productTypeId,
      })),
    })),
    ratings: data.ratings.map((rating: any) => ({
      id: rating.id,
      rating: rating.rating,
      review: rating.review,
      productId: rating.productId,
    })),
  };
};

export type {
  Product,
  ProductType,
  ProductImage,
  ProductRating,
  ProductQuery
}

export {
  convertProductToJson,
  convertJsonToProduct
}
