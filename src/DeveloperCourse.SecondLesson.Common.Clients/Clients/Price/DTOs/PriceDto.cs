using System;
using DeveloperCourse.SecondLesson.Domain.Types;

namespace DeveloperCourse.SecondLesson.Common.Clients.Clients.Price.DTOs
{
    public class PriceDto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public decimal Retail { get; set; }

        public Currency Currency { get; set; }
        
        public bool IsLast { get; set; }
    }
}