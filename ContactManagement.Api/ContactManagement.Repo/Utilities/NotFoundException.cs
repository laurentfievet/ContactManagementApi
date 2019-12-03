using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ContactManagement.Repo.Utilities
{
    [System.Serializable]
    public class NotFoundException : Exception
    {
        public long Id_Item { get; }

        public NotFoundException(long idItem) : this(idItem, null) { }

        public NotFoundException(long idItem, Exception inner) : base($"Unknow Item with Id [{idItem}]", inner) { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
