using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenXrm.Core.Database.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXrm.Core.Database.Repository
{
    public class UnitOfWork
    {
        protected CoreContext _context;
        public UnitOfWork(CoreContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Repository<Event> EventRepository
        {
            get
            {

                if (eventRepository == null)
                {
                    eventRepository = new Repository<Event>(_context);
                }
                return eventRepository;
            }
        }
    }
}
