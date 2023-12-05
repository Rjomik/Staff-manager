
using System;

namespace Server.DataAccess
{
    public class Worker
    {
        public virtual long Id { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual bool HasChildren { get; set; }
    }
    public enum Sex { Male = 0, Female = 1 }
}
