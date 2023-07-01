using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend.Extentions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
