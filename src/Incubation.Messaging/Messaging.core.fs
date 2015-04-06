namespace Incubation.Messaging
open System
open System.Threading
open System.Threading.Tasks

type IMessageSender =
  abstract SendAsync: message:'Msg -> Task<'Msg>

type IMessagePublisher =
  abstract PublishAsync: message:'Msg -> Task<'Msg>

type IMessageSubscriber =
  abstract SubscribeAsync :unit-> Action<'Msg>