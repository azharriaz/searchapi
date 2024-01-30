using System;
using CodingChallenge.Application.Common.Interfaces;

namespace CodingChallenge.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
