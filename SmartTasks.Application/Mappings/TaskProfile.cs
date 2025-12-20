using SmartTasks.Application.DTOs;
using SmartTasks.Domain.Entities;

namespace SmartTasks.Application.Mappings 
{
    public class TaskProfile : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskItem, TaskResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Attachment, AttachmentDto>();

            CreateMap<TaskCreateDto, TaskItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // set in service/controller
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore());

            CreateMap<TaskUpdateDto, TaskItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}
