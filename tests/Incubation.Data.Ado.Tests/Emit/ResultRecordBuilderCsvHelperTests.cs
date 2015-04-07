using System;
using System.IO;
using CsvHelper;
using FluentAssertions;
using Xunit;

namespace Incubation.Data.Emit
{
    public class ResultRecordBuilderCsvHelperTests
    {
        [Fact]
        public void CsvWriteWriteHeaderShouldProducesExpectedHeader()
        {
            var instance = ResultRecordBuilder.CreateRecord(
                RecordPropertyInfo.Create<int>("Count", 0),
                RecordPropertyInfo.Create<string>("Name", 1),
                RecordPropertyInfo.Create<DateTime>("Date", 2)
            );

            using (var textWriter = new StringWriter())
            {
                using (var csvWriter = new CsvWriter(textWriter))
                {
                    csvWriter.Configuration.HasHeaderRecord = true;
                    csvWriter.WriteHeader(instance.GetType());
                    var lines = textWriter.ToString().Split(new []{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                    var header = lines[0];
                    header.Should().Be("Count,Name,Date");
                }
            }
        }
    }
}