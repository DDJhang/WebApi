using Microsoft.EntityFrameworkCore;
using MyWebApi.Model;

namespace MyWebApi.Context
{
    public class PunchClockContext: DbContext
    {
        public PunchClockContext(DbContextOptions<PunchClockContext> options) : base(options)
        {
            
        }

        public virtual DbSet<OneDayPunchModel> punchs { get; set; }
    }
}
