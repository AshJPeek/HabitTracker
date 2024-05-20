using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Persistence.Models
{
    public record NumberOfSteps
    {
        public required int Id { get; init; }
        public required DateTime Date { get; init; }
        public required int Quantity { get; init; }
    }
}
