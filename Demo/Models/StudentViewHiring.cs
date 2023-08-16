using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Models
{
    public class StudentViewHiring
    {
        [JsonProperty("id")]
        [ValidateNever]
        public int id { get; set; }

        [JsonProperty("hiring_id")]
        [ValidateNever]
        public int hiring_id { get; set; }

        [JsonProperty("student_id")]
        [ValidateNever]
        public long student_id { get; set; }

        [JsonProperty("min_stipend")]
        [ValidateNever]
        public string min_stipend { get; set; }

        [JsonProperty("max_stipend")]
        [ValidateNever]
        public string max_stipend { get; set; }

        [JsonProperty("min_salary")]
        [ValidateNever]
        public string min_salary { get; set; }

        [JsonProperty("max_salary")]
        [ValidateNever]
        public string max_salary { get; set; }

        [JsonProperty("date_of_joining")]
        [ValidateNever]
        public string date_of_joining { get; set; }

        [ValidateNever]
        public int status { get; set; }

        [ValidateNever]
        public string remarks { get; set; }
        [ValidateNever]
        public string created_at { get; set; }

        [ValidateNever]
        public string updated_at { get; set; }

        [ValidateNever]
        public long cid { get; set; }

        [ValidateNever]
        public string label { get; set; }

        [ValidateNever]
        public string designation { get; set; }

        [ValidateNever]
        public string joblocation { get; set; }

        [ValidateNever]
        public string interview_mode { get; set; }

        [ValidateNever]
        public string interview_location { get; set; }

        [ValidateNever]
        public string cname { get; set; }

    }
}
