using System;
using System.Collections.Generic;

namespace SGMS.Models;

public partial class Student
{
    public string Studentid { get; set; } = null!;

    public string Studentname { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
