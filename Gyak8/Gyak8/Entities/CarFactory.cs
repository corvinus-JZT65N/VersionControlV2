using Gyak8.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak8.Entities
{
    class CarFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
