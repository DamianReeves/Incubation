using System;

namespace Incubation.Data.Emit
{
    public interface IRecordFactory
    {
        Type RecordType { get; }

        IResultRecord Create(params object[] parameters);
    }
}