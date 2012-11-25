using FlexProviders.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BCP.SimpleForum.Models
{
    public class Role : IFlexRole<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Users { get; set; }
    }
}