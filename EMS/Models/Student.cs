using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMS.Models;

public partial class Student
{
    public int StudentId { get; set; }

    [Required(ErrorMessage ="Name is Required")]
    [StringLength(20 , ErrorMessage ="PLease Fill the minimum requirement of {2} characters", MinimumLength = 5)]
    public string? StudentName { get; set; }

    [Required]
    [Range(1,12, ErrorMessage = "Class Should be between {1} to {2}" )]
    public int? StudentStandard { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "PLease Fill the minimum requirement of {2}  characters", MinimumLength = 5)]
    public string? StudentAddress { get; set; }

    [Required]
    [Range(1, 500, ErrorMessage = "Roll Number Should be between {1} to {2}")]
    public int? RollNo { get; set; }

    [Required]
    [Range(0, 5000, ErrorMessage = "Fees Should be between {1} to {2}")]
    public int? Fees { get; set; }
}
