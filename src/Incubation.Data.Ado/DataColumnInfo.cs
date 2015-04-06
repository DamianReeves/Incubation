using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incubation.Data.Ado
{
    public class DataColumnInfo: IEquatable<DataColumnInfo>
    {
        private readonly string _columnName;
        private readonly int _ordinal;
        private readonly Type _dataType;
        private readonly bool _isNullable;

        public DataColumnInfo(string columnName, int ordinal, Type dataType, bool isNullable=true)
        {
            if (columnName == null) throw new ArgumentNullException("columnName");
            if (dataType == null) throw new ArgumentNullException("dataType");
            _columnName = columnName;
            _ordinal = ordinal;
            _dataType = dataType;
            _isNullable = isNullable;
        }

        public string ColumnName
        {
            get { return _columnName; }
        }

        public int Ordinal
        {
            get { return _ordinal; }
        }

        public Type DataType
        {
            get { return _dataType; }
        }

        public bool IsNullable
        {
            get { return _isNullable; }
        }

        public static DataColumnInfo Create<TDataType>(string columnName, int ordinal, bool isNullable=true)
        {
            return new DataColumnInfo(columnName, ordinal, typeof(TDataType), isNullable);
        }

        public bool Equals(DataColumnInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_columnName, other._columnName) && _ordinal == other._ordinal && Equals(_dataType, other._dataType) && _isNullable.Equals(other._isNullable);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DataColumnInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_columnName != null ? _columnName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _ordinal;
                hashCode = (hashCode*397) ^ (_dataType != null ? _dataType.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _isNullable.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(DataColumnInfo left, DataColumnInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataColumnInfo left, DataColumnInfo right)
        {
            return !Equals(left, right);
        }
    }
}
