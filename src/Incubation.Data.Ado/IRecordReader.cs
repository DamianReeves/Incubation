namespace Incubation.Data
{
    public interface IRecordReader
    {
        
    }

    public interface IRecordWriter
    {
        
    }

    public interface IResultRecord : IRecordReader, IRecordWriter
    {
        
    }

    public abstract class ResultRecord : IResultRecord
    {
        
    }
}