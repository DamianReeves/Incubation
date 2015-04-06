using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Incubation.Data.Ado
{
    internal class ResultReader:IDataReader
    {
        private readonly IDataReader _wrappedReader;

        private ResultReader(IDataReader wrappedReader)
        {
            _wrappedReader = wrappedReader;
        }

        public static ResultReader Wrap(IDataReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            var resultReader = reader as ResultReader;
            if (resultReader == null)
            {
                return new ResultReader(reader);
            }
            return resultReader;
        }

        public static IDataReader Unwrap(IDataReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            var resultReader = reader as ResultReader;
            if (resultReader == null)
            {
                return reader;
            }
            return resultReader._wrappedReader;
        }

        public void Dispose()
        {
            _wrappedReader.Dispose();
        }

        public string GetName(int i)
        {
            return _wrappedReader.GetName(i);
        }

        public string GetDataTypeName(int i)
        {
            return _wrappedReader.GetDataTypeName(i);
        }

        public Type GetFieldType(int i)
        {
            return _wrappedReader.GetFieldType(i);
        }

        public object GetValue(int i)
        {
            return _wrappedReader.GetValue(i);
        }

        public int GetValues(object[] values)
        {
            return _wrappedReader.GetValues(values);
        }

        public int GetOrdinal(string name)
        {
            return _wrappedReader.GetOrdinal(name);
        }

        public bool GetBoolean(int i)
        {
            return _wrappedReader.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return _wrappedReader.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _wrappedReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return _wrappedReader.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _wrappedReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public Guid GetGuid(int i)
        {
            return _wrappedReader.GetGuid(i);
        }

        public short GetInt16(int i)
        {
            return _wrappedReader.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return _wrappedReader.GetInt32(i);
        }

        public long GetInt64(int i)
        {
            return _wrappedReader.GetInt64(i);
        }

        public float GetFloat(int i)
        {
            return _wrappedReader.GetFloat(i);
        }

        public double GetDouble(int i)
        {
            return _wrappedReader.GetDouble(i);
        }

        public string GetString(int i)
        {
            return _wrappedReader.GetString(i);
        }

        public decimal GetDecimal(int i)
        {
            return _wrappedReader.GetDecimal(i);
        }

        public DateTime GetDateTime(int i)
        {
            return _wrappedReader.GetDateTime(i);
        }

        public IDataReader GetData(int i)
        {
            return _wrappedReader.GetData(i);
        }

        public bool IsDBNull(int i)
        {
            return _wrappedReader.IsDBNull(i);
        }

        public int FieldCount
        {
            get { return _wrappedReader.FieldCount; }
        }

        object IDataRecord.this[int i]
        {
            get { return _wrappedReader[i]; }
        }

        object IDataRecord.this[string name]
        {
            get { return _wrappedReader[name]; }
        }

        public void Close()
        {
            _wrappedReader.Close();
        }

        public DataTable GetSchemaTable()
        {
            return _wrappedReader.GetSchemaTable();
        }

        public bool NextResult()
        {
            return _wrappedReader.NextResult();
        }

        public bool Read()
        {
            return _wrappedReader.Read();
        }

        public int Depth
        {
            get { return _wrappedReader.Depth; }
        }

        public bool IsClosed
        {
            get { return _wrappedReader.IsClosed; }
        }

        public int RecordsAffected
        {
            get { return _wrappedReader.RecordsAffected; }
        }
    }
}
