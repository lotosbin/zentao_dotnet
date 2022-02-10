using System.ComponentModel.DataAnnotations;

namespace zentao.api.Controllers;

public class AuthModel {
    [Required] public string account { get; set; }
    [Required] public string password { get; set; }
    [Required] public string host { get; set; }
}