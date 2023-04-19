using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskAssignmentApp.Application.Dtos
{
  public class EmployeeRequestDto
  {
    [JsonPropertyName("id")]
    public string Id { get; set; }


    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

  }
}
