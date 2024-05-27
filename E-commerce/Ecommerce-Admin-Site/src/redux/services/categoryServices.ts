import {  } from '../../Models/AuthModels';
import Axios from '../APIs/Axios';

const getAllCategoryService = async (): Promise<any> => {
  const data = await Axios.get('/category')
  return data;
};

const getCategoryByIdService = async (id: number): Promise<any> => {
  const data = await Axios.get(`/category/${id}`)
  return data;
};

const getCategoryParentsByIdService = async (id: number): Promise<any> => {
  const data = await Axios.get(`/category/${id}/parent-categories`)
  return data;
};

const updateCategoryByIdService = async (id: number, name: string, description?: string): Promise<any> => {
  const data = await Axios.put(`/category/${id}`, {
    name: name,
    description: description ?? ""
  })
  return data;
};

const createNewCategoryService = async (name: string, description?: string, parentCategoryId?: number | null): Promise<any> => {
  const data = await Axios.post('/category', {
    name: name,
    description: description ?? "",
    parentCategoryId: parentCategoryId ? parentCategoryId : null
  })
  return data;
};

const deleteCategoryByIdService = async (id: number): Promise<any> => {
  const data = await Axios.delete(`/category/${id}`)
  return data;
};

export {
  getAllCategoryService,
  getCategoryByIdService,
  getCategoryParentsByIdService,
  updateCategoryByIdService,
  createNewCategoryService,
  deleteCategoryByIdService
};
