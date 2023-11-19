using Microsoft.AspNetCore.Mvc.ModelBinding;

internal class ApiValidationErrorResponse : ModelStateDictionary
{
    public string[] Errors { get; set; }
}