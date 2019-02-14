using Shared.Database.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Database.Repositories
{
    public class EventRepository : IEventRepository
    {
        private RaveContext context;

        public EventRepository(RaveContext context)
        {
            this.context = context;
        }
        public void Delete(int id)
        {
            context.Events.Remove(GetById(id));
        }

        public IEnumerable<Event> GetActiveEvents(int count)
        {
            return context.Events.Where(d => d.EndDate != DateTime.Now).Take(count).ToList();
        }

        public Event GetById(int id)
        {
            return context.Events.Single(i => i.EventId == id);
        }

        public IEnumerable<Event> GetEvents()
        {
            return context.Events.ToList();
        }

        public void Insert(Event @event)
        {
            context.Events.Add(@event);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Event @event)
        {
            context.Events.Update(@event);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~EventRepository() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
