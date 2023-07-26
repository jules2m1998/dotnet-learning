using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Models.DTOs.DifficultyDTO;
using NZWalks.API.Models.DTOs.ImagesDTO;
using NZWalks.API.Models.DTOs.WalkDTO;

namespace NZWalks.API.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Region, AddRegionRequestDto>().ReverseMap();
        CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();

        CreateMap<Walk, AddWalksRequestDto>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();


        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        CreateMap<Image, ImageUploadRequestDto>().ReverseMap();
        CreateMap<Image, ImageDto>().ReverseMap();
    }
}
