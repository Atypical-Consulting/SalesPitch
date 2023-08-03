using System.ComponentModel;
using SalesPitch.Services.Language;
using Spectre.Console.Cli;

namespace SalesPitch.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class SalesPitchSettings : CommandSettings
{
    [CommandArgument(0, "[product]")]
    [Description("The product to advertise")]
    public string? Product { get; set; }
        
    [CommandArgument(1, "[price]")]
    [Description("The price of the product")]
    public string? Price { get; set; }
        
    [CommandArgument(2, "[features]")]
    [Description("The features of the product")]
    public string? Features { get; set; }
        
    [CommandArgument(3, "[benefits]")]
    [Description("The benefits of the product")]
    public string? Benefits { get; set; }

    [CommandOption("-f|--framework")]
    [Description("The sales framework to use")]
    [DefaultValue(SalesPitchFramework.Storytelling)]
    public SalesPitchFramework? Framework { get; set; }
        
    [CommandOption("-l|--language")]
    [Description("The language to use")]
    [DefaultValue(SupportedLanguage.English)]
    public SupportedLanguage? Language { get; set; }
        
    [CommandOption("-d|--demo")]
    [DefaultValue(false)]
    [Description("Use demo data")]
    public bool IsDemo { get; set; }
}