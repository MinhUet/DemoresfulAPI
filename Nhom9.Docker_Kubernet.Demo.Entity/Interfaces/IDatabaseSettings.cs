using System;
using System.Collections.Generic;
using System.Text;

namespace Nhom9.Docker_Kubernet.Demo.Entity.Interfaces
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
