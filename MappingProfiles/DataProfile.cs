using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Contexts;
using UniWall.Data.Entities;
using UniWall.Exceptions;
using UniWall.Models.Requests;
using UniWall.Models.Responses;
using UniWall.Utilities;

namespace UniWall.MappingProfiles
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<ApiException, ErrorResponse>();

            CreateMap<HttpException, ErrorsListResponse>();

            CreateMap<Exception, UnexpectedErrorResponse>();

            CreateMap<UploadedFile, UploadResponse>();

            CreateMap<Subject, SubjectPartialResponse>();

            CreateMap<Lecturer, LecturerPartialResponse>()
                .ForMember(
                    dest => dest.Name,
                    operation => operation.MapFrom(src => src.FirstName + " " + src.LastName)
                );

            CreateMap<Training, TrainingResponse>()
                .ForMember(
                    dest => dest.Time,
                    operation => operation.MapFrom(source => TimeUtil.GetSeconds(source.Date))
                )
                .ForMember(
                    dest => dest.Location,
                    operation => operation.MapFrom(source => new LocationResponse()
                    {
                        Type = source.IsOnline ? "REMOTELY" : "STATIONARY",
                        Place = source.IsOnline ? "Online" : source.Address.ToString()
                    })
                )
                .ForMember(
                    dest => dest.Seats,
                    operation => operation.MapFrom(source => source.MaximumAttendees - source.Attendees.Count)
                );

            CreateMap<AddressRequest, Address>();

            CreateMap<TrainingRequest, Training>()
                .ForMember(
                    dest => dest.Date,
                    operation => operation.MapFrom(source => TimeUtil.FromSeconds(source.Time))
                )
                .ForMember(dest => dest.IsOnline, operation => operation.MapFrom(source => source.Online))
                .ForMember(dest => dest.Lecturers, operation => operation.Ignore())
                .ForMember(dest => dest.Subjects, operation => operation.Ignore());

            CreateMap<Training, Training>()
                .ForMember(dest => dest.Id, operation => operation.Ignore());

            CreateMap<LecturerRequest, Lecturer>()
                .ForMember(dest => dest.Subjects, operation => operation.Ignore());

            CreateMap<Lecturer, LecturerResponse>()
                .ForMember(dest => dest.Name, operation => operation.MapFrom(source => source.FirstName + " " + source.LastName));

        }
    }
}
