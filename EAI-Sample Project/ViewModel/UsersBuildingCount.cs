using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAI_Sample_Project.ViewModel
{
    public class UsersBuildingCount
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CountBuildings { get; set; }
    }
}