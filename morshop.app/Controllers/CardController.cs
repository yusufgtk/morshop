using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using morshop.app.Identity;
using morshop.app.Models;
using morshop.business.Abstract;
using morshop.entity;

namespace morshop.app.Controllers
{
    public class CardController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICardService _cardService;
        private readonly IOrderService _orderService;

        public CardController(UserManager<AppUser> userManager, ICardService cardService,IOrderService orderService)
        {
            _userManager=userManager;
            _cardService=cardService;
            _orderService=orderService;
        }
        public IActionResult Index()
        {
            var card = _cardService.GetCardByUserId(_userManager.GetUserId(User));
            return View(new CardModel(){
                CardId=card.Id,
                CardItems=card.CardItems.Select(i=>new CardItemModel(){
                    CardItemId=i.Id,                                       
                    ProductId=i.Product.Id,
                    Name=i.Product.Name,
                    Price=i.Product.CurrentPrice,
                    ImageUrl=i.Product.ImageUrl,
                    Quantity=i.Quantity

                }).ToList()
            });
        }
        [HttpPost]
        public IActionResult AddCard(int ProductId, int Quantity)
        {
            var userId = _userManager.GetUserId(User);//oturum açmış kullanıcının id sini getirir
            if(userId!=null)
            {
                _cardService.AddCard(userId,ProductId,Quantity);
            }
            return RedirectToAction("Index","Card");
        }

        [HttpPost]
        public IActionResult DeleteCardItem(int ProductId)
        {
            Console.WriteLine(ProductId);
            var userId = _userManager.GetUserId(User);//oturum açmış kullanıcının id sini getirir
            Console.WriteLine(userId);
            
            if(userId!=null)
            {
                _cardService.DeleteCard(userId,ProductId);
            }
            return RedirectToAction("Index","Card");
        }

        public IActionResult Orders()
        {
            var orders = _orderService.GetOrders(_userManager.GetUserId(User));
            var orderListModel = new List<OrderListModel>();
            OrderListModel orderModel;
            if(orders!=null)
            {
                foreach(var order in orders)
                {
                    orderModel = new OrderListModel();
                    orderModel.OrderNumber=order.OrderNumber;
                    orderModel.OrderDate=order.OrderDate;
                    orderModel.Note=order.Note;
                    orderModel.Phone=order.Phone;
                    orderModel.FirstName=order.FirstName;
                    orderModel.LastName=order.LastName;
                    orderModel.Address=order.Address;
                    orderModel.City=order.City;

                    orderModel.OrderItemsModel = order.OrderItems?.Select(i => new OrderItemModel {
                        OrderItemId = i.Id,
                        Name = i.Product?.Name, // Product null ise, null olmayan bir değer atar
                        Price = (double)i.Price,
                        Quantity = i.Quantity,
                        ImageUrl = i.Product?.ImageUrl
                    }).ToList() ?? new List<OrderItemModel>();
                    
                    orderListModel.Add(orderModel);
                }
            }
            
            return View("Orders",orderListModel);
        }


        public IActionResult Checkout()
        {
            var card = _cardService.GetCardByUserId(_userManager.GetUserId(User));
            var orderModel=new OrderModel();
            if(ModelState.IsValid)
            {
                orderModel.CardModel=new CardModel()
                {
                    CardId=card.Id,
                    CardItems=card.CardItems.Select(i=>new CardItemModel(){
                        CardItemId=i.Id,                                       
                        ProductId=i.Product.Id,
                        Name=i.Product.Name,
                        Price=i.Product.CurrentPrice,
                        ImageUrl=i.Product.ImageUrl,
                        Quantity=i.Quantity

                    }).ToList()
                };
            }
            
            return View(orderModel);
        }

        //iyzico odeme - api entegresi
        [HttpPost]
        public IActionResult Checkout(OrderModel orderModel)
        {
            if(!ModelState.IsValid)
            {
                var userId =_userManager.GetUserId(User);
                var card = _cardService.GetCardByUserId(userId);
                orderModel.Email="abc@abc.com";
                orderModel.Note="";
                orderModel.CardModel = new CardModel()
                {
                    CardId=card.Id,
                    CardItems=card.CardItems.Select(i=>new CardItemModel()
                    {
                        CardItemId=i.Id,                                       
                        ProductId=i.Product.Id,
                        Name=i.Product.Name,
                        Price=i.Product.CurrentPrice,
                        ImageUrl=i.Product.ImageUrl,
                        Quantity=i.Quantity
                    }).ToList()
                };
                var payment = PaymentProcess(orderModel,_userManager.GetUserId(User));
                if(payment.Status=="success")
                {
                    SaveOrder(orderModel,payment,userId);
                    ClearCard(userId);
                    return View("Success");
                }
                else
                {
                    Console.WriteLine(payment.ErrorCode);
                    Console.WriteLine(payment.ErrorMessage);
                    Console.WriteLine(payment.ErrorGroup);
                }
            }

            

            return View(orderModel);
        }

        private void ClearCard(string userId)
        {
            var card = _cardService.GetCardByUserId(userId);
            _cardService.ClearCard(card.Id);
        }

        private void SaveOrder(OrderModel orderModel, Payment payment, string userId)
        {
            var order = new Order();
            order.OrderNumber=new Random().Next(111111,999999).ToString();
            order.OrderState=EnumOrderState.completed;
            order.PaymentTypes=EnumPaymentTypes.CreditCard;
            order.PaymentId=payment.PaymentId;
            order.ConversationId=payment.ConversationId;
            order.OrderDate=new DateTime();
            order.FirstName=orderModel.FirstName;
            order.LastName=orderModel.LastName;
            order.UserId=userId;
            order.Address=orderModel.Address;
            order.Phone=orderModel.Phone;
            order.City=orderModel.City;
            order.Email=orderModel.Email;
            order.Note=orderModel.Note;
            order.OrderItems=new List<entity.OrderItem>();
            foreach(var item in orderModel.CardModel.CardItems)
            {
                var orderItem=new entity.OrderItem()
                {
                    Price=item.Price,
                    Quantity=item.Quantity,
                    ProductId=item.ProductId

                };
                order.OrderItems.Add(orderItem);
            }
            _orderService.Create(order);
        }
        
        private Payment PaymentProcess(OrderModel orderModel,string userId)
        {
            Iyzipay.Options options = new Iyzipay.Options();
            options.ApiKey = "sandbox-3UIatHX5Nsmru7NtRxqqGKYTkN1J045L";
            options.SecretKey = "sandbox-OPywwpVjkoBSSLKZMwhdPpaLTL31e33v";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
                    
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111,999999999).ToString();
            request.Price = orderModel.CardModel.TotalPrice().ToString();
            request.PaidPrice = orderModel.CardModel.TotalPrice().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = orderModel.CardModel.CardId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = orderModel.FullName;
            paymentCard.CardNumber = orderModel.CardNumber;
            paymentCard.ExpireMonth = orderModel.ExpirationMonth;
            paymentCard.ExpireYear = orderModel.ExpirationYears;
            paymentCard.Cvc = orderModel.Cvc.ToString();
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            // paymentCard.CardNumber = "5528790000000008";
            // paymentCard.ExpireMonth = "12";
            // paymentCard.ExpireYear = "2030";
            // paymentCard.Cvc = "123";

            Buyer buyer = new Buyer();
            buyer.Id = userId;
            buyer.Name = orderModel.FirstName;
            buyer.Surname = orderModel.LastName;
            buyer.GsmNumber = orderModel.Phone;
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = orderModel.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = orderModel.City;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = orderModel.FirstName+" "+orderModel.LastName;
            shippingAddress.City = orderModel.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = orderModel.Address;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = orderModel.FirstName+" "+orderModel.LastName;
            billingAddress.City = orderModel.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = orderModel.Address;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            BasketItem basketItem;

            foreach(var item in orderModel.CardModel.CardItems)
            {
                basketItem=new BasketItem();
                basketItem.Id=item.ProductId.ToString();
                basketItem.Name=item.Name;
                basketItem.Price=((double)item.Price).ToString();
                basketItem.Category1="a";
                basketItem.ItemType=BasketItemType.PHYSICAL.ToString();
                
                basketItems.Add(basketItem);
            }
            
            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }


    }

}