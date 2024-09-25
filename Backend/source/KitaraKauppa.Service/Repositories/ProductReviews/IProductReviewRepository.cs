using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitaraKauppa.Core.ProductReviews;
using KitaraKauppa.Service.ProductReviewsService;

namespace KitaraKauppa.Service.Repositories.ProductReviews
{
    public interface IProductReviewRepository
    {
        public Task<bool> CreateReview(ProductReviewCreateDto reviewCreateDto);
        public Task<bool> UpdateReview(Guid reviewId, string review, int star);
        public Task<bool> DeleteReview(Guid reviewId);
        public Task<ProductReview> GetSingleReview(Guid reviewId);
        public Task<List<ProductReview>> GetReviewsByProduct(Guid productId);
        public Task<List<ProductReview>> GetReviewsByUser(Guid userId);
    }
}