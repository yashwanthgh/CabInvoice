using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoice
{
    public interface IRideRepository
    {
        IEnumerable<IRide> GetRidesForUser(int userId);
    }
    public class RideRepository : IRideRepository
    {
        private Dictionary<int, List<IRide>> UserRides;

        public IEnumerable<IRide> GetRidesForUser(int userId)
        {
            if (UserRides.TryGetValue(userId, out List<IRide>? value))
            {
                return value;
            }

            return [];
        }
    }
}
