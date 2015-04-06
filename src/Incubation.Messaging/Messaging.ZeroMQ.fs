namespace Incubation.Messaging.ZeroMQ
open System
open System.IO
open System.Threading.Tasks
open NetMQ

module ZeroMQMessagingSystem =
  open System
  open System.IO
  open System.Threading.Tasks
  open NetMQ  

  type ServerConnection = {
    FrontendUri: string
    BackendUri: string
  }

  let StartServer connection =
    let {FrontendUri=frontendUri; BackendUri=backendUri} = connection
    use context = NetMQContext.Create()
    use xpubSocket = context.CreateXPublisherSocket()
    use xsubSocket = context.CreateXSubscriberSocket()

    xpubSocket.Bind(backendUri)
    xsubSocket.Bind(frontendUri)

    Console.WriteLine("Intermediary started, and waiting for messages")
    let proxy = new Proxy(xsubSocket, xpubSocket, null)
    let handle = 
      { new System.IDisposable with
        member x.Dispose() =
          context.Dispose()
        }
    proxy.Start()     
    handle
