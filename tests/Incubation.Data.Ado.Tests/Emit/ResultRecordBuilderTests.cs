using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Incubation.Data.Emit
{
    public class ResultRecordBuilderTests
    {
        [Fact]
        public void CreateRecordForSinglePropertyShouldWork()
        {
            var instance = ResultRecordBuilder.CreateRecord(
                RecordPropertyInfo.Create<int>("Count", 0)
            );
            var instanceType = instance.GetType();
            instanceType.GetProperties()
                .Should().HaveCount(1)
                .And.Subject.Single().ShouldBeEquivalentTo(new {Count=0}, opt=>opt.ExcludingMissingMembers());
        }

        [Fact]
        public void CreateRecordForTwoProperties()
        {
            var instance = ResultRecordBuilder.CreateRecord(
                RecordPropertyInfo.Create<int>("Count", 0),
                RecordPropertyInfo.Create<string>("Name", 1)
            );
            instance.ShouldBeEquivalentTo(
                new {Count=default(int),Name=default(string)},
            opt=>opt.IncludingAllRuntimeProperties()
            );
        }

        [Fact]
        public void CreateRecordCanBeCalledMultipleTimes()
        {
            var instance1 = ResultRecordBuilder.CreateRecord(
                RecordPropertyInfo.Create<int>("Count", 0),
                RecordPropertyInfo.Create<string>("Name", 1)
            );

            var instance2 = ResultRecordBuilder.CreateRecord(
                RecordPropertyInfo.Create<string>("FooString", 0),
                RecordPropertyInfo.Create<DateTime>("FooDate", 1),
                RecordPropertyInfo.Create<bool>("FooBool", 2)
            );
        }

        public class GetFactoryTests
        {
            [Fact]
            public void GetFactoryShouldReturnFactoryWithRecordTypeAssignableToIResultRecord()
            {
                var factory = new[]
                {
                    RecordPropertyInfo.Create<string>("Name", 0)
                }.GetRecordFactory();

                factory.RecordType.Should().BeAssignableTo<IResultRecord>();
            } 
        }
    }
}
