using System;

namespace Incubation.Data
{
    public class RecordPropertyInfo: IEquatable<RecordPropertyInfo>
    {
        private readonly string _name;
        private readonly int _ordinal;
        private readonly Type _propertyType;
        private readonly bool _isNullable;

        public RecordPropertyInfo(string name, int ordinal, Type propertyType, bool isNullable=true)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (propertyType == null) throw new ArgumentNullException("propertyType");
            _name = name;
            _ordinal = ordinal;
            _propertyType = propertyType;
            _isNullable = isNullable;
        }

        public string Name
        {
            get { return _name; }
        }

        public int Ordinal
        {
            get { return _ordinal; }
        }

        public Type PropertyType
        {
            get { return _propertyType; }
        }

        public bool IsNullable
        {
            get { return _isNullable; }
        }

        public static RecordPropertyInfo Create<TPropertyType>(string name, int ordinal, bool isNullable=true)
        {
            return new RecordPropertyInfo(name, ordinal, typeof(TPropertyType), isNullable);
        }

        public bool Equals(RecordPropertyInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_name, other._name) && _ordinal == other._ordinal && Equals(_propertyType, other._propertyType) && _isNullable.Equals(other._isNullable);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RecordPropertyInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_name != null ? _name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _ordinal;
                hashCode = (hashCode*397) ^ (_propertyType != null ? _propertyType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _isNullable.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(RecordPropertyInfo left, RecordPropertyInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(RecordPropertyInfo left, RecordPropertyInfo right)
        {
            return !Equals(left, right);
        }
    }
}
