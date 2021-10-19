using AutoMapper;
using WordCount.Core.Models;
using WordCount.Core.Resources;

namespace WordCountSolution.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Text, TextResource>();
            CreateMap<Text, SaveTextResource>();

            //Resource to Domain
            CreateMap<TextResource, Text>();
            CreateMap<SaveTextResource, Text>();
        }
    }
}