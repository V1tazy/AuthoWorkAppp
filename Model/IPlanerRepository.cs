using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoWorkAppp.Model
{
    internal interface IPlanerRepository
    {
        void Edit(string id);
        void Remove(string id);
        void Add(PlanerEventModel planerEvent, string userId);

        IEnumerable<PlanerEventModel> GetAll(string username);
    }
}
