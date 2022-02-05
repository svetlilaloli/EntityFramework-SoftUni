namespace TeisterMask
{
    using AutoMapper;
    using System;
    using System.Globalization;
    using System.Linq;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using TeisterMask.DataProcessor.ImportDto;

    public class TeisterMaskProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public TeisterMaskProfile()
        {
            // import
            CreateMap<TaskInputDto, Task>()
                .ForMember(dest => dest.OpenDate, opt => opt.MapFrom(t => DateTime.ParseExact(t.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(t => DateTime.ParseExact(t.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.ExecutionType, opt => opt.MapFrom(t => Enum.Parse<ExecutionType>(t.ExecutionType)))
                .ForMember(dest => dest.LabelType, opt => opt.MapFrom(t => Enum.Parse<LabelType>(t.LabelType)));
            CreateMap<ProjectInputDto, Project>()
                .ForMember(dest => dest.OpenDate, opt => opt.MapFrom(p => DateTime.ParseExact(p.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(p => p.DueDate != null ? DateTime.ParseExact(p.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null));
            CreateMap<EmployeeInputDto, Employee>()
                .ForMember(dest => dest.EmployeesTasks, opt => opt.MapFrom(e => e.Tasks.Select(t => new EmployeeTask { TaskId = t})));
            
            // export
        }
    }
}
