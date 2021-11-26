using Gyak8.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak8.Abstractions
{
    public interface IToyFactory
    {
       string Név { get; }
        Toy CreateNew();
    }
}
