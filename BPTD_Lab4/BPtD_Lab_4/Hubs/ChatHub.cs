using System.Collections.Concurrent;
using System.Threading.Tasks;
using AesEncryptionTool;
using DiffieHellmanTool;
using Microsoft.AspNetCore.SignalR;

namespace BPtD_Lab_4.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, Keys> _keys;

        static ChatHub()
        {
            _keys = new ConcurrentDictionary<string, Keys>();
        }

        public async Task GetKeys()
        {
            var q = DiffieHellmanService.GetQ();
            var a = DiffieHellmanService.GetA(q);
            var x = DiffieHellmanService.GetX(q);
            var y = DiffieHellmanService.GetY(a, x, q);

            var keys = new Keys(q, a, x, y);

            _keys.TryAdd(Context.ConnectionId, keys);

            await Clients.Caller.SendAsync("ReceiveKeys", q, a, (int)y);
        }

        public async Task ReceiveKey(int y)
        {
            var keys = _keys[Context.ConnectionId];

            keys.Key = DiffieHellmanService.GetKey(y, keys.X, keys.Q);
        }

        public async Task SendMessage(string userName, string message)
        {
            var key = _keys[Context.ConnectionId];

            var decryptedMessage = AesUtil.Decrypt(message, ((int)key.Key).ToString());

            await Clients.All.SendAsync("Chat", userName, decryptedMessage);
        }
    }
}
