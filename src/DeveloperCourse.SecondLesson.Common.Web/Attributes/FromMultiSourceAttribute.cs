using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeveloperCourse.SecondLesson.Common.Web.Attributes
{
    public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource { get; } = CompositeBindingSource.Create(
            new[]
            {
                BindingSource.Path, BindingSource.Query
            },
            nameof(FromMultiSourceAttribute));
    } 
}