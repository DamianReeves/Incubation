namespace Incubation.Reflection.Emit
open System

type PropertyMetadata = {
  Name:string
  PropertyType:Type
  Order:int
}  

type TypeModel = {
  Name:string;
  Properties:PropertyMetadata list
}

type TypeDef =
| Record of Name:string * Properties:PropertyMetadata list

[<AutoOpen>]
module Metadata =
  open System

  let property propertyType name = 
    { 
      PropertyType=propertyType;
      Name=name;Order=0;
    }

  let dataRecord name properties =
    { 
      Name=name;
      Properties= Seq.toList properties;
    }