import { KKAutoCompleteOptionType } from "../../components/Shared/KKAutoComplete";
import { QueryOptions } from "./commonTypes";

export enum ProductOrderOptions {
  ProductTitle = "ProductTitle",
  Price = "Price",
}

export interface ProductQueryOptions extends QueryOptions<ProductOrderOptions> {
  varientType: string;
}

export interface ProductDto {
  productId: string;
  productTitle: string;
  brandName: string;
  imageAlt: string;
  imageUrl: string;
  price: number;
}

export interface ProductDefinitionDto {
  brands: BrandDto[];
  colors: ColorDto[];
  oriantations: string[];
  variants: string[];
}

export interface BrandDto {
  id: string;
  name: string;
}

export interface ColorDto {
  id: string;
  name: string;
}

export enum ProductVariant {
  electric = "Electric",
  accoustic = "Accoustic",
  semiAccoustic = "SemiAccoustic",
}

export enum ProductOrientation {
  rightHanded = "RightHanded",
  leftHanded = "LeftHanded",
  bothOptions = "BothOptions",
}

export interface ProductForm {
  id?: string;
  title: string;
  description: string;
  brandId: KKAutoCompleteOptionType;
  unitPrice: number;
  varientType: KKAutoCompleteOptionType;
  orientation: KKAutoCompleteOptionType;
  productColors: KKAutoCompleteOptionType;
  imageName: string;
  imageExtension: string;
}

export interface NewProductSaveRequest {
  title: string;
  description: string;
  brandId: string;
  unitPrice: number;
  varientType: string;
  productOrientation: string;
  productColors: string[];
  imageName: string;
  extension: string;
}

export interface ExistingProduct extends NewProductSaveRequest {
  id: string;
}

export interface UploadedProductImage {
  name: string;
  extension: string;
}
