using System.ComponentModel.DataAnnotations;

namespace SalesPitch.WebApp.Models;

public class SalesPitchRequest
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    public string Product { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Price is required")]
    [StringLength(50, ErrorMessage = "Price cannot exceed 50 characters")]
    public string Price { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Features are required")]
    [StringLength(500, ErrorMessage = "Features cannot exceed 500 characters")]
    public string Features { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Benefits are required")]
    [StringLength(500, ErrorMessage = "Benefits cannot exceed 500 characters")]
    public string Benefits { get; set; } = string.Empty;
    
    public SalesPitchFramework Framework { get; set; } = SalesPitchFramework.Storytelling;
    
    public SupportedLanguage Language { get; set; } = SupportedLanguage.English;
    
    public bool IsDemo { get; set; }
}