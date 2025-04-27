using System;
using System.Collections.Generic;

namespace SGMS.Models;

public partial class Course
{
    public string Courseid { get; set; } = null!;

    public string Coursename { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
