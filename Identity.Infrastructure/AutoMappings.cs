using AutoMapper;
using Identity.Core.Entities;
using Identity.Core.ViewModels;
using System;

namespace Identity.Infrastructure
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<UserRequestModel, UserEntity>()
                .ForMember(target => target.UserName, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
