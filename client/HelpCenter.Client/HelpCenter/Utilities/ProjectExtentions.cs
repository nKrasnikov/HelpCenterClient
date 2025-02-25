using System.Diagnostics.CodeAnalysis;
using Common.Models.Response;
using HelpCenter.Models;
using Refit;

namespace HelpCenter.Utilities
{
    /// <summary>
    /// Project Model Extentions
    /// </summary>
    public static class ProjectExtentions
    {
        /// <summary>
        /// Check if the project is null or new.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static bool IsNullOrNew([NotNullWhen(false)] this Project? project)
        {
            return project is null || project.ID == 0;
        }
    }

    public static class RefitExtensions
    {
        public static async Task<ErrorResponse?> Err(this ApiException ex)
        {
            return await ex.GetContentAsAsync<ErrorResponse>();
        }
    }
}