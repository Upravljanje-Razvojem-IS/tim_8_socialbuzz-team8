using AutoMapper;
using ProfileService.Models;
using ProfileService.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserDetails, UserDetailsDto>();
            CreateMap<UserDetailsDto, UserDetails>();
            CreateMap<City, CityDto>().ForMember(
                dest => dest.Country,
                opt => opt.MapFrom(src => $"{src.Country.CountryName}"));
            CreateMap<CityDto, City>();
            CreateMap<CountryDto, Country>();
            CreateMap<CountryCreationDto, Country>();
            CreateMap<Country, CountryDto>();
            CreateMap<CityMutationDto, CityDto>();
            CreateMap<CityMutationDto, City>();
            CreateMap<CorporateUserDetailsDto, CorporateUserDetails>();
            CreateMap<CorporateUserDetails, CorporateUserDetailsDto>();
            CreateMap<UserCreationDto, UserDetails>();

        }
    }
}
