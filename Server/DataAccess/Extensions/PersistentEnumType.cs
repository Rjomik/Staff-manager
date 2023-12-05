using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DataAccess
{
    /// <summary>
    /// For correct value comparison and int4 conversion
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentEnumType<T> : PersistentEnumType
    {
        public PersistentEnumType() : base(typeof(T))
        {
        }
    }
}
