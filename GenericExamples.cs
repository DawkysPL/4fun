using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp3
{
    public class GenericExamples
    {
        public void Action()
        {

        }
    }

    public interface IAggregateBase<TKey>
    {
        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent;

        IEnumerable<IEvent> GetEventsToPublish();
    }

    public interface IRepositoryBase<TAggregate, TKey> where TAggregate : IAggregateBase<TKey>
    {
        public void StartTransaction();//przekazany dataContext id
        public void Commit();

        public void Rollback();

        public void Save();
    }

    public abstract class RepositoryBase<TAggregate, TKey> : IRepositoryBase<TAggregate, TKey> where TAggregate : IAggregateBase<TKey>
    {
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void StartTransaction()
        {
            throw new NotImplementedException();
        }
    }

    public class UserRepository : RepositoryBase<User, Guid>
    {

    }

    public class User : AggregateBase<Guid>
    {

        public User() : base()
        {
            Type = GetType().Name.ToString(); //todo change it  
        }
        public string Name { get; set; }

        public string Login { get; set; }

        public string PhoneNumber { get; set; }

        public User CreateUser(string name, string login, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Login = login;
            Name = name;
            PhoneNumber = phoneNumber;

            Publish(new UserCreated { UserId = Id });

            return this;
        }
    }

    public abstract class AggregateBase<TKey> : IAggregateBase<TKey>
    {
        public AggregateBase()
        {
            LastEventNo = 0;
        }
        public TKey Id { get; set; }

        public string Type { get; set; }

        public int LastEventNo { get; set; }

        private List<IEvent> events = new List<IEvent>();

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            @event.AggregateId = Id.ToString();
            @event.AggregateName = Type;
            @event.No = ++LastEventNo;

            events.Add(@event);
        }

        public IEnumerable<IEvent> GetEventsToPublish()
        {
            var cache = events.Select(x => x);
            events.Clear();

            return cache;
        }
    }

    public interface IEvent
    {
        string AggregateName { get; set; }
        string AggregateId { get; set; }
        int No { get; set; }
    }

    public class UserCreated : EventBase
    {
        public Guid UserId { get; set; }
    }

    public abstract class EventBase : IEvent
    {
        public string AggregateName { get; set; }
        public string AggregateId { get; set; }
        public int No { get; set; }
    }

}
