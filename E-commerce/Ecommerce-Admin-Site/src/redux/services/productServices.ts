import { Product, ProductQuery, convertJsonToProduct } from '../../Models/ProductModel';
import Axios from '../APIs/Axios';

const getAllProductService = async (query: ProductQuery): Promise<any> => {
  const data = await Axios.get('/product', { params: query });
  return data;
};

const getProductByIdService = async (id: number): Promise<Product> => {
  const data = await Axios.get(`/product/${id}`);
  return convertJsonToProduct(data);
};

const updateProductByIdService = async (product: Product): Promise<Product> => {
  const data = await Axios.put(`/product/${product.id}`, product);
  return convertJsonToProduct(data);
};

const createNewProductService = async (product: Product): Promise<Product> => {
  const data = await Axios.post('/product', product);
  return convertJsonToProduct(data);
};

const deleteProductByIdService = async (id: number): Promise<void> => {
  await Axios.delete(`/product/${id}`);
};

export {
  getAllProductService,
  getProductByIdService,
  updateProductByIdService,
  createNewProductService,
  deleteProductByIdService
};
