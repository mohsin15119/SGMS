using System;
using System.Collections.Generic;

namespace SGMS.Models;

public partial class Instructor
{
    public string Instructorid { get; set; } = null!;

    public string Instructorname { get; set; } = null!;

    public string? Instructoremail { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
