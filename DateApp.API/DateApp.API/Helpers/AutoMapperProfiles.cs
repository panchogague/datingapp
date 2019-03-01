using AutoMapper;
using DateApp.API.Dto;
using DateApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserModel, UserForListDTO>()
                .ForMember(dest=> dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });
            CreateMap<UserModel, UserForDetailDTO>()
                .ForMember(dest => dest.PhotoUrl, opt => {
                     opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                }).ForMember(dest => dest.Age, opt => {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                });  
            CreateMap<PhotoModel, PhotosForDetailDTO>();
            CreateMap<UserForUpdateDTO, UserModel>();
        }
    }
}
