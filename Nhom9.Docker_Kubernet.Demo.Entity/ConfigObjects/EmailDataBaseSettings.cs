using Nhom9.Docker_Kubernet.Demo.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nhom9.Docker_Kubernet.Demo.Entity.ConfigObjects
{
    public class EmailDataBaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
