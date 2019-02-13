using System;

namespace Shared.Models
{
    [Flags]
    public enum EventType
    {
        Club = 1,
        OpenAir = 2,
        Festival = 4
    }
}
