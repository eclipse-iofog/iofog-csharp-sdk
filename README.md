# container-sdk-csharp

This module lets you easily build an ioElement. It gives you all the functionality to interact with ioFog via Local API. Additionally some useful methods to work with ioMessage.

 - send new message to ioFog
 - fetch next unread messages from ioFog
 - fetch messages for time period and list of accessible publishers
 - get config options
 - connect to ioFog Control Channel via WebSocket
 - connect to ioFog Message Channel via WebSocket

## Code snippets:

`IoFogRestApiClient` implements all methods to communicate with ioFog (via local REST API).
`IoFogWebSocketApiClient` implements all methods to communicate with ioFog (via local WS API).
`IoMessage` represent all message communication between ioFog and Containers.
`IoMessageUtils` class is convenient to encode and decode byte arrays.
`IIoFogRestApiHandler` and `IIoFogWebSocketApiHandler` - interfaces for handling requests to ioFog via REST and WS APIs respectively.

Set up custom host, port and container's ID (in case of no params default values for host and port will be used: 'iofog', 54321):
```cs
	var restHandler = new RestHandler(); // implementation of the IIoFogRestApiHandler interface
    var restClient = new IoFogRestApiClient(restHandler, "iofog", 54321); // creating instance of REST API ioFog client

	var wsHandler = new WsHandler(); // implementation of the IIoFogWebSocketApiHandler interface
    var restClient = new IoFogRestApiClient(wsHandler, "iofog", 54321); // creating instance of WS API ioFog client
```

You can also use custom ContainerId for testing purposes by specifying `SELFNAME` environment variable.

#### REST calls (could trigger OnException, OnBadRequest handler's methods):
Post new IoMessage to ioFog via REST call
```cs
	var message = new IoMessage();
	var handler = new RestHandler(); // implementation of the IIoFogRestApiHandler interface
	var client = new IoFogRestApiClient(handler); // creating instance of REST API ioFog client
	var response = await client.PostMessageAsync(message);
```

Get list of IoMessages by time frame for accessible publishers from ioFog via REST call
```cs
	var handler = new RestHandler(); // implementation of the IIoFogRestApiHandler interface
	var client = new IoFogRestApiClient(handler); // creating instance of REST API ioFog client
	long timeframeStart = 1234567890123;
	long timeframeEnd = 1234567890123;
	string[] publishers = new[] { "sefhuiw4984twefsdoiuhsdf", "d895y459rwdsifuhSDFKukuewf", "SESD984wtsdidsiusidsufgsdfkh" };
	var response = await client.GetMessagesFromPublishersWithinTimeframeAsync(timeframeStart, timeframeEnd, publishers);
```

Get list of next unread ioMessages via REST call
```cs
	var handler = new RestHandler(); // implementation of the IIoFogRestApiHandler interface
	var client = new IoFogRestApiClient(handler); // creating instance of REST API ioFog client
	var response = await client.GetContainerNextUnreadMessagesConfigAsync();
```

Get container's config via REST call
```cs
	var handler = new RestHandler(); // implementation of the IIoFogRestApiHandler interface
	var client = new IoFogRestApiClient(handler); // creating instance of REST API ioFog client
	var response = await client.GetContainerConfigAsync();
```

#### WebSocket calls  (could trigger OnException, OnMessage, OnNewConfigSignal and OnReceipt listener's methods):

Open WS Control Channel to ioFog
```cs
	var handler = new WsHandler(); // implementation of the IIoFogWebSocketApiHandler interface
    var messageClient = new IoFogWebSocketApiClient(handler);
	await messageClient.ConnectAsync(IoFogWebSocketEndpointEnum.ControlSocket); // opens ws connection to specified endpoint
	await messageClient.StartListenAsync(); // starts listen to connected endpoint
```
Open WS Message Channel to ioFog
```cs
	var handler = new WsHandler(); // implementation of the IIoFogWebSocketApiHandler interface
    var messageClient = new IoFogWebSocketApiClient(handler);
	await messageClient.ConnectAsync(IoFogWebSocketEndpointEnum.MessageSocket); // opens ws connection to specified endpoint
	await messageClient.StartListenAsync(); // starts listen to connected endpoint
```
Send IoMessage via WS Message Channel (pre-condition: WS Message Channel is open):
```cs
  var message = new IoMessage();
  await messageClient.SendMessageAsync(message);
```