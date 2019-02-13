namespace Shared.Models.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToString(this Event @event)
        {
            //TODO: return format
            return string.Concat(@event.Name, @event.StartDate.ToString(), @event.EndDate.ToString());
        }
    }
}
