using AutoMapper;

using LibraryApp.Application.RequestQuery;
using LibraryApp.Domain;

namespace LibraryApp.Application.MappingProfiles;

public class RequestToDomainProfile : Profile
{
    public RequestToDomainProfile()
    {
        CreateMap<PaginationDetails, PaginationFilter>();
    }

}
