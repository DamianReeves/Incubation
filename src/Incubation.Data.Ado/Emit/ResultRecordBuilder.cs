using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
//using PropertyAttributes = System.Reflection.PropertyAttributes;

namespace Incubation.Data.Emit
{
    public static class ResultRecordBuilder
    {
        public static object CreateRecord(params RecordPropertyInfo[] properties)
        {
            return CreateRecord((IEnumerable<RecordPropertyInfo>) properties);
        }

        public static object CreateRecord(IEnumerable<RecordPropertyInfo> properties)
        {
            var factory = GetRecordFactory(properties);
            return factory.Create();
        }

        public static IRecordFactory GetRecordFactory(this IEnumerable<RecordPropertyInfo> properties)
        {
            var recordType = CompileResultType(properties);
            return new RecordFactory(recordType);
        }

        public static TypeBuilder BuildType(IEnumerable<RecordPropertyInfo> properties)
        {
            TypeBuilder tb = GetTypeBuilder();
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            foreach (var property in properties)
                CreateProperty(tb, property.Name, property.PropertyType);

            return tb;
        }

        public static Type CompileResultType(IEnumerable<RecordPropertyInfo> properties)
        {
            var tb = BuildType(properties);
            Type objectType = tb.CreateType();

            return objectType;
        }

        public static void WriteRecordAssembly(string path, params RecordPropertyInfo[] properties)
        {
            var tb = BuildType(properties);
        }

        private static TypeBuilder GetTypeBuilder()
        {
            var typeSignature = "MyDynamicType";
            var an = new AssemblyName("Generated.DataRecords");
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("Generated.DataRecords");
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature
                , TypeAttributes.Public |
                  TypeAttributes.Class |
                  TypeAttributes.AutoClass |
                  TypeAttributes.AnsiClass |
                  TypeAttributes.BeforeFieldInit |
                  TypeAttributes.AutoLayout
                , typeof(ResultRecord));

            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                    MethodAttributes.Public |
                    MethodAttributes.SpecialName |
                    MethodAttributes.HideBySig,
                    null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }
}