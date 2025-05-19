using AutoMapper;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Dtos.Modify;
using N5.Permissions.Domain.Dtos.Request;
using N5.Permissions.Domain.Entities;

namespace N5.Permissions.Application.Configurations.Mappers;

/// <summary>
/// Mapper for the <see cref="Permission"/> class.
/// This class is responsible for mapping between the <see cref="Permission"/> entity and the <see cref="ModifyPermissionCommandDto"/> data transfer object.
/// </summary>
public class PermissionMapper : Profile
{
    /// <summary>
    /// Constructor to initialize the configuration profile.
    /// </summary>
    public PermissionMapper()
    {
        CreateMap<Permission, ModifyPermissionDto>();
        CreateMap<ModifyPermissionDto, Permission>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PermissionTypeId, opt => opt.Ignore());

        CreateMap<Permission, PermissionDto>()
            .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => src.PermissionTypeId));

        CreateMap<RequestPermissionCommandDto, Permission>()
            .ForMember(dest => dest.PermissionTypeId, opt => opt.MapFrom(src => src.PermissionType));
    }
}
