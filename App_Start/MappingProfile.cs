using AutoMapper;
using Prac1Proj.DTO;
using Prac1Proj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prac1Proj.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDTO>();
            Mapper.CreateMap<CustomerDTO, Customer>();
            Mapper.CreateMap<Movie, MovieDTO>();
            Mapper.CreateMap<MovieDTO, Movie>();

        }
    }
}