using AutoMapper;
using idea_generic_task_server.Core;
using zentao.client;

namespace idea_generic_task_server {
    public class BugProfile : Profile {
        public BugProfile() {
            var map = CreateMap<BugItem, Task>();
            map.ForMember(dest => dest.summary, opt => opt.MapFrom(src => $"{src.title}"));
            map.ForMember(dest => dest.closed, opt => opt.MapFrom(src => src.status == "closed"));
            map.ForMember(dest => dest.type, opt => opt.MapFrom(src => "Bug"));
        }
    }
}