using SampleRestWebApi.Contracts.V1.Responses;
using SampleRestWebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace SampleRestWebApi.Mapping
{
    public class DomainToResponseProfiles : Profile
    {
        public DomainToResponseProfiles()
        {
            CreateMap<Client, ClientResponse>()
                .ForMember(dest => dest.Tags, opt =>
                    opt.MapFrom(src => src.ClientTags.Select(r => new TagResponse { Name = r.Tag.Name })));
            CreateMap<Tag, TagResponse>();
        }
    }
}
