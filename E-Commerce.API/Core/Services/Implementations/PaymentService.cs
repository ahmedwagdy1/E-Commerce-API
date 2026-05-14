using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using Stripe;
using Product = Domain.Entities.ProductModule.Product;

namespace Services.Implementations
{
    public class PaymentService(IConfiguration _configuration, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSetting")["SecretKey"];
            var basket = await GetBasketAsync(basketId);
            await ValidationBasketAsync(basket);
            var amount = CalculateTotalAmount(basket);
            await CreateOrUpdatePaymentIntentId(basket, amount);
            await _basketRepository.AddOrUpdateAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }

        private async Task CreateOrUpdatePaymentIntentId(CustomerBasket basket, long amount)
        {
            var StripeService = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var paymentIntent = await StripeService.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = amount,
                };
                await StripeService.UpdateAsync(basket.PaymentIntentId, options);
            }
        }

        private long CalculateTotalAmount(CustomerBasket basket)
        {
            return (long)(basket.Items.Sum(a => a.Quantity * a.Price) + basket.ShippingPrice) * 100;
        }

        private async Task ValidationBasketAsync(CustomerBasket basket)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;
        }

        private async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return await _basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);
        }

        //public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        //{
            //// 1. Set Up Key [secret key] => stripe key
            //StripeConfiguration.ApiKey = _configuration.GetSection("StripeSetting")["SecretKey"];
            //// 2. Get Basket [BasketId]
            //var basket = await _basketRepository.GetBasketAsync(basketId) 
            //    ?? throw new BasketNotFoundException(basketId);
            //// 3. Validation Items Price [BasketItem.Price = Product.Price]
            //foreach (var item in basket.BasketItems)
            //{
            //    var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) 
            //        ?? throw new ProductNotFoundException(item.Id);
            //    item.Price = product.Price;
            //}
            //// 4. Validation Shipping Price => Get Delivery Method [Shipping.Price = DeliveryMethod.Price]
            //if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");
            //var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
            //    .GetByIdAsync(basket.DeliveryMethodId.Value)
            //    ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            //basket.ShippingPrice = deliveryMethod.Price;
            //// 5. Total = [SubTotal + ShippingPrice] ==> cent ==> Long (Casting)
            //var amount = (long) (basket.BasketItems.Sum(a => a.Quantity * a.Price) + basket.ShippingPrice) * 100;
            //// 6. Create Or Update PaymentIntentId
            //var StripeService = new PaymentIntentService();
            //if (string.IsNullOrEmpty(basket.PaymentIntentId))
            //{
            //    // create
            //    var options = new PaymentIntentCreateOptions()
            //    {
            //        Amount = amount,
            //        Currency = "USD",
            //        PaymentMethodTypes = ["card"]
            //    };
            //    var paymentIntent = await StripeService.CreateAsync(options);
            //    basket.PaymentIntentId = paymentIntent.Id;
            //    basket.ClientSecret = paymentIntent.ClientSecret;
            //}
            //else
            //{
            //    // update
            //    var options = new PaymentIntentUpdateOptions()
            //    {
            //        Amount = amount,
            //    };
            //    await StripeService.UpdateAsync(basket.PaymentIntentId, options);
            //}
            //// 7. Save Changes [Update Basket]
            //await _basketRepository.AddOrUpdateAsync(basket);
            //// 8. Map To BasketDto ==> return
            //return _mapper.Map<BasketDto>(basket);
        //}
    }
}
