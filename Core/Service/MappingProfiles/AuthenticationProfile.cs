namespace Service.MappingProfiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
