using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EAI_Sample_Project.ViewModel
{
    public class BuildingAccess
    {
        [Required]
        public int BuildingID { get; set; }
        public string Name { get; set; }
        public bool Access { get; set; }
    }
}