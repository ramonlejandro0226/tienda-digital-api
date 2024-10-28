using api_tienda_digital.Context.CreatedStatusDTO;
using api_tienda_digital.Context.CreatedUserExtendDTO;
using api_tienda_digital.Context.Models;
using api_tienda_digital.Context.UpdatedStatusOrderDTO;
using api_tienda_digital.DTOs.CreatedOrderDTO;
using api_tienda_digital.DTOs.CreatedPaymentMethodDTO;
using api_tienda_digital.DTOs.CreatedProductDTO;
using api_tienda_digital.DTOs.OrderDTO;
using api_tienda_digital.DTOs.UpdatedPaymentMethodDTO;
using api_tienda_digital.DTOs.UpdatedProductDTO;
using api_tienda_digital.DTOs.UserExtendDTO;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeo para CreatedOrderDTO
        CreateMap<CreatedOrderDTO, Order>().ReverseMap();
        CreateMap<OrderDetailCreatedOrderDTO, OrderDetail>().ReverseMap();
        CreateMap<OrderProductCreatedOrderDTO, OrderProduct>().ReverseMap();
        CreateMap<PaymentMethodCreatedOrderDTO, PaymentMethod>().ReverseMap();
        CreateMap<ProductCreatedOrderDTO, Product>().ReverseMap();
        CreateMap<StatusCreatedOrderDTO, Status>().ReverseMap();

        // Mapeo para OrderDTO
        CreateMap<Order, OrderDTO>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailOrderDTO>().ReverseMap();
        CreateMap<OrderProduct, OrderProductOrderDTO>().ReverseMap();
        CreateMap<PaymentMethod, PaymentMethodOrderDTO>().ReverseMap();
        CreateMap<Product, ProductOrderDTO>().ReverseMap();
        CreateMap<Status, StatusOrderDTO>().ReverseMap();

        // Mapeo para PaymentMethodDTO
        CreateMap<CreatedPaymentMethodDTO, PaymentMethod>().ReverseMap();
        CreateMap<UpdatedPaymentMethodDTO, PaymentMethod>().ReverseMap();

        // Mapeo para ProductDTO
        CreateMap<CreatedProductDTO, Product>().ReverseMap();
        CreateMap<UpdatedProductDTO, Product>().ReverseMap();

        // Mapeo para StatusDTO
        CreateMap<CreatedStatusDTO, Status>().ReverseMap();
        CreateMap<UpdatedStatusDTO, Status>().ReverseMap();

        // Mapeo para UserExtend
        CreateMap<CreatedUserExtendDTO, UserExtend>().ReverseMap();
        CreateMap<UserExtend, UserExtendDTO>().ReverseMap();
    }
}
