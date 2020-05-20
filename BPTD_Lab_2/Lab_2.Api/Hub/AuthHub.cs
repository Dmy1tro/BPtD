using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RsaEncryptionTool;

namespace Lab_2.Api.Hub
{
    public class AuthHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private static readonly ConcurrentDictionary<string, RsaKey> ClientKeys;

        static AuthHub()
        {
            ClientKeys = new ConcurrentDictionary<string, RsaKey>();
        }

        public async Task GetPublicKey()
        {
            var rsaKey = ClientKeys[Context.ConnectionId];

            await Clients.Caller.SendAsync("PublicKey", rsaKey.PrivateKey);
        }

        public async Task Auth(string login, string password)
        {
            var privateKey = ClientKeys[Context.ConnectionId];

            var decryptedText = RsaUtil.Decrypt(password, privateKey.PrivateKey);

            await Clients.Caller.SendAsync("AuthResult", decryptedText);
        }

        public override Task OnConnectedAsync()
        {
            var keyForUser = RsaUtil.GenerateRsaKey();

            ClientKeys.TryAdd(Context.ConnectionId, keyForUser);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (ClientKeys.ContainsKey(Context.ConnectionId))
            {
                ClientKeys.TryRemove(Context.ConnectionId, out _);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
