import Grid from "@mui/material/Grid2";
import { CSSProperties, useEffect, useMemo, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import KKTextBox from "../../components/Shared/KKTextBox";
import { useForm } from "react-hook-form";
import KKAutoComplete, {
  KKAutoCompleteOptionType,
} from "../../components/Shared/KKAutoComplete";
import KKButton from "../../components/Shared/KKButton";
import SaveIcon from "@mui/icons-material/Save";
import {
  useAddProductMutation,
  useGetDefinitionsQuery,
  useGetProductByIdQuery,
  useUploadProductImageMutation,
} from "../../features/api/apiSlice";
import { KKResult } from "../../features/types/commonTypes";
import {
  BrandDto,
  ColorDto,
  NewProductSaveRequest,
  ProductForm,
} from "../../features/types/productTypes";
import { formatCamelCase } from "../../features/utilities/textSeperator";
import KKFileUpload from "../../components/Shared/KKFileUpload";

interface ProductType {
  productId: string;
  productTitle: string;
  price: number;
  description: string;
  imageId: string[];
}

const AddEditProducts = () => {
  const containerStyle: CSSProperties = {
    marginLeft: "350px",
    marginTop: "20px",
    paddingRight: "20px",
  };
  const { productId } = useParams();
  const navigate = useNavigate();

  const [existingProduct, setExistingProduct] = useState<ProductForm | null>(
    null
  );

  const [uploadedFileName, setUploadedFileName] = useState<string>("");
  const [uploadedFileExtension, setUploadedFileExtension] =
    useState<string>("");

  const {
    data: retrivedProduct,
    error,
    isLoading: isSavedProductLoading,
  } = useGetProductByIdQuery(productId as string, {
    skip: !productId,
  });

  const {
    data: definitionData,
    error: definitionError,
    isLoading: definitionIsLoading,
  } = useGetDefinitionsQuery();

  const [addProduct, { isLoading }] = useAddProductMutation();
  const [uploadProductImage] = useUploadProductImageMutation();

  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
    setValue,
  } = useForm<ProductForm>();

  const brandOptions = useMemo(() => {
    return definitionData?.succeeded
      ? definitionData.value?.brands?.map((brand: BrandDto) => ({
          label: brand.name,
          id: brand.id,
        })) ?? []
      : [];
  }, [definitionData]);

  const colorOptions = useMemo(() => {
    return definitionData?.succeeded
      ? definitionData.value?.colors?.map((color: ColorDto) => ({
          label: color.name,
          id: color.id,
        })) ?? []
      : [];
  }, [definitionData]);

  const orientationOptions = useMemo(() => {
    return definitionData?.succeeded
      ? definitionData.value?.oriantations?.map((orientation) => ({
          label: formatCamelCase(orientation),
          id: orientation,
        })) ?? []
      : [];
  }, [definitionData]);

  const variantOptions = useMemo(() => {
    return definitionData?.succeeded
      ? definitionData.value?.variants?.map((variant) => ({
          label: formatCamelCase(variant),
          id: variant,
        })) ?? []
      : [];
  }, [definitionData]);

  useEffect(() => {
    if (retrivedProduct) {
      const product = retrivedProduct.value;

      setValue("title", product!.title, {
        shouldValidate: true,
        shouldDirty: false,
      });

      setExistingProduct({
        id: product!.id,
        title: product!.title,
        description: product!.description,
        unitPrice: parseFloat(product!.unitPrice.toString()),
        brandId: brandOptions.find((brand) => brand.id === product!.brandId)!,
        varientType: variantOptions.find(
          (variant) => variant.id === product!.varientType
        )!,
        orientation: orientationOptions.find(
          (orientation) => orientation.id === product!.productOrientation
        )!,
        productColors: colorOptions.find(
          (color) => color.id === product!.productColors[0]
        )!,
        imageExtension: "",
        imageName: "",
      });
    }

    setTimeout(() => {
      console.log("existingProduct", existingProduct);
    }, 1000);
  }, [retrivedProduct]);

  const onSubmit = async (data: ProductForm) => {
    const saveRequest: NewProductSaveRequest = {
      title: data.title,
      description: data.description,
      brandId: data.brandId.id,
      unitPrice: data.unitPrice,
      varientType: data.varientType.id,
      productOrientation: data.orientation.id,
      productColors: [data.productColors.id],
      imageName: data.imageName,
      extension: data.imageExtension,
    };

    const result = await addProduct(saveRequest).unwrap();

    if (result.succeeded) {
      alert("Product saved successfully");
      navigate("/admin/products");
    } else {
      console.log("Product save failed");
    }
  };
  const handleFileUpload = async (files: FileList | null) => {
    if (files && files.length > 0) {
      const file = files[0];
      const formData = new FormData();
      formData.append("file", file);

      const results = await uploadProductImage(formData).unwrap();

      if (results.succeeded) {
        setUploadedFileName(results.value!.name);
        setUploadedFileExtension(results.value!.extension);
      }
    }
  };

  return (
    <Grid style={containerStyle} size={12}>
      <h2>{productId ? "Edit Product" : "Add New Product"}</h2>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container size={12} spacing={2}>
          <Grid size={6}>
            <KKTextBox
              Id="productTitle"
              Label="Product Title"
              Type="text"
              {...register("title", { required: true })}
            />
          </Grid>
          <Grid size={6}>
            <KKAutoComplete
              id="brandId"
              options={brandOptions}
              label="Brand"
              register={register}
              setValue={setValue}
              registerName="brandId"
            />
          </Grid>
          <Grid size={12}>
            <KKTextBox
              Id="description"
              Label="Description"
              Value=""
              Type="text"
              {...register("description", { required: true })}
            />
          </Grid>
          <Grid size={6}>
            <KKTextBox
              Id="price"
              Label="Price"
              Value=""
              Type="number"
              {...register("unitPrice", { required: true })}
            />
          </Grid>
          <Grid size={6}>
            <KKAutoComplete
              id="variant"
              options={variantOptions}
              label="Variant"
              register={register}
              setValue={setValue}
              registerName="varientType"
            />
          </Grid>
          <Grid size={6}>
            <KKAutoComplete
              id="orientation"
              options={orientationOptions}
              label="Orientation"
              register={register}
              setValue={setValue}
              registerName="orientation"
            />
          </Grid>
          <Grid size={6}>
            <KKAutoComplete
              id="productColors"
              options={colorOptions}
              label="Colors"
              register={register}
              setValue={setValue}
              registerName="productColors"
            />
          </Grid>
          <Grid size={6}>
            <KKFileUpload
              OnFileUpload={handleFileUpload}
              UploadedFileName={uploadedFileName}
              UploadedFileExtension={uploadedFileExtension}
              register={register}
            />
          </Grid>
          <Grid size={12} textAlign="right">
            <KKButton
              Text="Save"
              Variant="contained"
              Icon={<SaveIcon />}
              Type="submit"
            />
          </Grid>
        </Grid>
      </form>
    </Grid>
  );
};

export default AddEditProducts;
