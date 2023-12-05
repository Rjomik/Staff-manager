using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using GrpcApi;
using NHibernate.Type;

namespace Server.DataAccess
{
    internal class WorkerMap : ClassMap<Worker>
    {
        public WorkerMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.MiddleName);
            Map(x => x.Birthday).Not.Nullable();
            Map(x => x.Sex).CustomType(typeof(PersistentEnumType<Sex>));
            Map(x => x.HasChildren).Not.Nullable();
        }
    }
}
