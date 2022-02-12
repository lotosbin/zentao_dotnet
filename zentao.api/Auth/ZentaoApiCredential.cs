using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace zentao.api.Auth;

public class ZentaoApiCredential {
    [Required] public string account { get; set; }
    [Required] public string password { get; set; }
    [Required] public string host { get; set; }
}