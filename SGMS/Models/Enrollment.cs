using System;
using System.Collections.Generic;

namespace SGMS.Models;

public partial class Enrollment
{
    public string Enrollmentid { get; set; } = null!;

    public string Studentid { get; set; } = null!;

    public string Courseid { get; set; } = null!;

    public string Instructorid { get; set; } = null!;

    public string? Grade { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Instructor Instructor { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
