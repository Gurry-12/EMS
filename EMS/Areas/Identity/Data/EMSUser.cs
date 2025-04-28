using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EMS.Areas.Identity.Data;

// Add profile data for application users by adding properties to the EMSUser class
public class EMSUser : IdentityUser
{
    public string? FullName { get; set; }  // Example custom property
}

