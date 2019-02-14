using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Database.Interfaces
{
    public interface IEventRepository : IDisposable 
    {
        Event GetById(int id);
        IEnumerable<Event> FindEvent(string eventName);
        IEnumerable<Event> GetActiveEvents(int count);
        IEnumerable<Event> GetEvents();
        void Insert(Event @event);
        void Delete(int id);
        void Update(Event @event);
        void Save();
    }
}
