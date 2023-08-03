﻿namespace Demo.Models
{
    public class ViewHiring
    {
        public IEnumerable<Company> Companies { get; set; }
        public IEnumerable<Sessions> Session { get; set; }
        public IEnumerable<Hiring> Hirings { get; set; }
        public IEnumerable<Student> students { get; set; }
        public IEnumerable<Hiring_Departments> hiring_Departments { get; set; }
        public IEnumerable<StudentApplication> applications { get; set; }

    }
}
