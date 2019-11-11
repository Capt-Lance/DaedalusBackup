﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackupManagement.Domain
{
    public abstract class Entity
    {
        public List<INotification> DomainEvents { get; private set; }

        public void AddDomainEvent(INotification eventItem)
        {
            DomainEvents = DomainEvents ?? new List<INotification>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            DomainEvents.Remove(eventItem);
        }


    }
}
