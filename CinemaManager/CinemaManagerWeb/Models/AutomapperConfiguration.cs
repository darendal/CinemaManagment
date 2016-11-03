using CinemaManager;
using CinemaManagerWeb.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaManagerWeb.Models
{
    public class AutomapperConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Cinema, CinemaDTO>().ReverseMap();

                cfg.CreateMap<Theater, TheaterDTO>().ReverseMap();

                cfg.CreateMap<Movie, MovieDTO>().ReverseMap();

                cfg.CreateMap<MovieLease, MovieLeaseDTO>().ReverseMap();

            });
        }

        public static void Test()
        {
            AutoMapper.Mapper.AssertConfigurationIsValid();
        }
    }
}