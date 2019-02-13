using System;

namespace Shared.Models
{
    [Flags]
    public enum EventType : int
    {
        Club = 1,
        OpenAir = 2,
        Festival = 4
    }
}
