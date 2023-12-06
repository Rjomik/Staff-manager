using GrpcApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Worker
    {
        public Worker()
        {

        }

        public Worker(WorkerMessage grpcSource)
        {
            Id = grpcSource.Id;
            LastName = grpcSource.LastName;
            FirstName = grpcSource.FirstName;
            MiddleName = grpcSource.MiddleName;
            Birthday = new DateTime( grpcSource.Birthday, DateTimeKind.Utc);
            Sex = (Sex)(int)grpcSource.Sex;
            HasChildren = grpcSource.HasChildren;
        }

        public virtual long Id { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual bool HasChildren { get; set; }

        public virtual WorkerMessage ToWorkerMessage()
        {
            return new WorkerMessage()
            {
                Id = Id,
                LastName = LastName,
                FirstName = FirstName,
                MiddleName = MiddleName,
                Birthday = Birthday.Ticks,
                Sex = (GrpcApi.Sex)(int)Sex,
                HasChildren = HasChildren
            };
        }
    }
    public enum Sex { Male = 0, Female = 1 }
}
