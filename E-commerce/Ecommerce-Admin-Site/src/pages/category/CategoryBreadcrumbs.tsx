import { useState, useEffect } from 'react';
import Breadcrumbs from '@mui/material/Breadcrumbs';
import Link from '@mui/material/Link';
import Typography from '@mui/material/Typography';
import { getCategoryParentsByIdService } from '../../redux/services/categoryServices'; // import your API service
import { Category } from '../../Models/CategoryModels';

interface Breadcrumb {
  name: string;
  id: number;
}

export default function CategoryBreadcrumbs({ categoryId }: { categoryId: number | null }) {
  const [breadcrumbs, setBreadcrumbs] = useState<Breadcrumb[]>([]);

  useEffect(() => {
    if (categoryId) {
      fetchCategoryBreadcrumbs(categoryId);
    }
  }, [categoryId]);

  const fetchCategoryBreadcrumbs = async (id: number) => {
    try {
      const categoryParents = await getCategoryParentsByIdService(id);
      const breadcrumbNames = categoryParents.map((category: Category) => ({
        name: category.name,
        id: category.id,
      }));
      setBreadcrumbs(breadcrumbNames);
    } catch (error) {
      console.error('Error fetching category breadcrumbs:', error);
    }
  };

  return (
    <div>
      <Breadcrumbs maxItems={2} aria-label="breadcrumb">
        <Link underline="hover" color="inherit" href="/categories">
          Main Category
        </Link>
        {breadcrumbs.map((breadcrumb, index) => {
          const isLast = index === breadcrumbs.length - 1;
          return isLast ? (
            <Typography key={index} color="text.primary">
              {breadcrumb.name}
            </Typography>
          ) : (
            <Link key={index} underline="hover" color="inherit" href={`/categories/${breadcrumb.id}`}>
              {breadcrumb.name}
            </Link>
          );
        })}
      </Breadcrumbs>
    </div>
  );
}
