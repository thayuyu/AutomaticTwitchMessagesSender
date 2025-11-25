using System.Text.Json;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchAutomaticMessageSend.Models;

var accountName =           Environment.GetEnvironmentVariable("AccountName") ?? throw new ApplicationException("AccountName is not set.");
var refreshToken =          Environment.GetEnvironmentVariable("RefreshToken") ?? throw new ApplicationException("RefreshToken is not set.");
var broadcasterUsername =   Environment.GetEnvironmentVariable("BroadcasterName") ?? throw new ApplicationException("BroadcasterName is not set.");
var clientId =              Environment.GetEnvironmentVariable("ClientId") ?? throw new ApplicationException("ClientId is not set.");
var clientSecret =          Environment.GetEnvironmentVariable("ClientSecret") ?? throw new ApplicationException("ClientSecret is not set.");

var authToken = await RefreshTokenAsync();

var credentials = new ConnectionCredentials(
        accountName,
        $"oauth:{authToken}"
);


var client = new TwitchClient();
client.Initialize(credentials, broadcasterUsername);

client.OnConnected += OnConnected;
client.OnDisconnected += OnDisconnected;
client.OnMessageReceived += OnMessageReceived;


await client.ConnectAsync();
Console.WriteLine("Connected.\n(Press Enter to stop listening)");

Console.ReadKey();

Console.WriteLine("Disconnecting...");
await client.DisconnectAsync();

async Task OnConnected(object? sender, OnConnectedEventArgs e)
{
    Console.WriteLine($"Connected to channel with account:\t{e.BotUsername}");
    await client.SendMessageAsync(broadcasterUsername, "Listener connected.");
}

async Task OnDisconnected(object? sender, OnDisconnectedArgs e)
{
    Console.WriteLine($"Disconnected from channel with account:\t{e.BotUsername}");
    await client.SendMessageAsync(broadcasterUsername, "Connection closed.");
}

async Task OnMessageReceived(object? sender, OnMessageReceivedArgs e)
{
    var msg = e.ChatMessage.Message.ToLower();

    if (msg.Contains("67"))
    {
        Console.WriteLine($"{e.ChatMessage.Username} said 67. big W\t- {DateTime.Now}");
        await client.SendMessageAsync(broadcasterUsername, "67?!?!?");
    }
    else if (msg.Contains("61"))
    {
        Console.WriteLine($"{e.ChatMessage.Username} is starting a new meme.\t- {DateTime.Now}");
        await client.SendMessageAsync(broadcasterUsername, "61 61 61 61");
    }
}

async Task<string> RefreshTokenAsync()
{
    var refreshAuthTokenUel = "https://id.twitch.tv/oauth2/token";

    // changed from json to fit x-www-form-urlencoded content type
    var requestBody = new Dictionary<string, string>
    {
        { "grant_type", "refresh_token" },
        { "refresh_token", refreshToken },
        { "client_id", clientId },
        { "client_secret", clientSecret }
    };

    HttpClient client = new();

   var content = new FormUrlEncodedContent(requestBody);

    var response = await client.PostAsync(refreshAuthTokenUel, content);

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine(response.ReasonPhrase);
        Console.WriteLine(response.RequestMessage);
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        throw new Exception("Could not refresh token.");
    }

    var newAuthToken = JsonSerializer.Deserialize<RefreshTokenResp>(await response.Content.ReadAsStringAsync())?.AccessToken ?? throw new Exception("Something went wrong in the deserialisation.");

    return newAuthToken;
}