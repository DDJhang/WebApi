using Dapper.FluentMap.Mapping;
using MyWebApi.Model;

namespace MyWebApi.Repository.Map
{
    public class PunchClockMap : EntityMap<OneDayPunchModel>
    {
        public PunchClockMap()
        {
            Map(p => p.Account).ToColumn("account");
            Map(p => p.Name).ToColumn("name");
            Map(p => p.PunchIn).ToColumn("punchin");
            Map(p => p.PunchOut).ToColumn("punchout");
        }
    }
}
