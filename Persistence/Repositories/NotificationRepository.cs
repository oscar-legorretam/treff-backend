using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;
using Persistence.Repositories.Base;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(treff_v2Context treffContext) : base(treffContext) { }

        public async Task<IEnumerable<Notification>> GetNotificationByFreelancerIdAsync(int freelancerId, bool onlyUnread)
        {
            var notifications = await _treffContext.Notifications
                .Where(n => n.UserId == freelancerId
                    && n.Read == !onlyUnread)
                .Include(n => n.Freelancer)
                .ToListAsync();
            return notifications;
        }

        public async Task<IEnumerable<Notification>> ClearNotificationsByFreelancerIdAsync(int freelancerId)
        {
            var notifications = await _treffContext.Notifications
                .Where(n => n.UserId == freelancerId
                    && n.Read == false)
                .ToListAsync();

            notifications.ForEach(a =>
            {
                a.Read = true;
            });

            _treffContext.SaveChanges();

            return notifications;
        }
    }
}
