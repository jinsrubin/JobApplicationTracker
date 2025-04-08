using AutoMapper;
using JobApplicationTracker.Server.Entities;
using JobApplicationTracker.Server.Models.DTOs;

namespace JobApplicationTracker.Server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobApplication, JobApplicationResponse>()
                .ReverseMap();
            CreateMap<JobApplicationRequest, JobApplication>()
                .ReverseMap();
        }
    }
}
