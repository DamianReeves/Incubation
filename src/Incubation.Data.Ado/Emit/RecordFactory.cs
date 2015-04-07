using System;
using System.Linq;

namespace Incubation.Data.Emit
{
    internal class RecordFactory:IRecordFactory
    {
        private readonly Type _recordType;
        private readonly Func<Type, object[], IResultRecord> _activator;

        public RecordFactory(Type recordType):this(recordType, null)
        {            
        }
        public RecordFactory(Type recordType, Func<Type, object[], IResultRecord> activator)
        {
            if (recordType == null) throw new ArgumentNullException("recordType");
            var interfaces = recordType.FindInterfaces((type, criteria) => type == typeof (IResultRecord),null);
            if(interfaces.Length < 1) throw new ArgumentException("The record type must implement the IResultRecord interface.", "recordType");
            _recordType = recordType;
            _activator = activator ?? ((type, parameters) =>
            {
                var instance = System.Activator.CreateInstance(recordType, parameters);
                return (IResultRecord) instance;
            });
        }

        public Type RecordType
        {
            get { return _recordType; }
        }

        public Func<Type, object[], IResultRecord> Activator
        {
            get { return _activator; }
        }

        public IResultRecord Create(params object[] parameters)
        {
            return Activator.Invoke(RecordType, parameters);
        }
    }
}