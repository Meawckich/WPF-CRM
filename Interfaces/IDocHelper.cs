using BuildingMaterials.DataDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingMaterials.Interfaces
{
    public interface IDocHelper
    {
        public FileInfo FileInfo { get;}
        public string Path { get; }
        void Open();
        void Precess();
    }
}
