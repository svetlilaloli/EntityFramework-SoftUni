﻿namespace TeisterMask
{
    using AutoMapper;
    using TeisterMask.Data.Models;
    using TeisterMask.DataProcessor.ImportDto;

    public class TeisterMaskProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public TeisterMaskProfile()
        {
            // import
            CreateMap<TaskInputDto, Task>();
            CreateMap<ProjectInputDto, Project>();
            CreateMap<EmployeeInputDto, Employee>();
            
            // export
        }
    }
}
