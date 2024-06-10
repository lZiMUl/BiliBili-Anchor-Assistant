using System.Net.WebSockets;

namespace BiliBili_Anchor_Assistant.Tools
{
    public enum EventTypeEnum
    {
        Join,
        Message,
        Gift
    }

    public interface IJoin
    {
        public string Uid { get; init; }
        public string Username { get; init; }
        public string JoinTime { get; init; }
    }
    public interface IMessage : IJoin
    {
        public string Message { get; init; }
    }
    public interface IGift : IJoin
    {
        public string GiftName { get; init; }
        public string Gift { get; init; }
        public string Message { get; init; }
    }

    public class BiliAPI : Event<string>
    {
        private readonly ClientWebSocket _clientWebSocket = new();

        public BiliAPI()
        {
            _clientWebSocket.ConnectAsync(new Uri("ws://localhost:6969/ws"),
                CancellationToken.None);
            AddEventListener("start", status =>
            {

            });
            AddEventListener("stop", status =>
            {

            });

        }

        public void Start()
        {
            Emit("start", string.Empty);
        }

        public void Stop()
        {
            Emit("stop", string.Empty);
        }
        public void AddListener(EventTypeEnum eventTypeEnum, Action<string> callback)
        {
            switch (eventTypeEnum)
            {
                case EventTypeEnum.Join:
                    {
                        AddEventListener("join", callback);
                    }
                    break;

                case EventTypeEnum.Message:
                    {
                        AddEventListener("message", callback);
                    }
                    break;

                case EventTypeEnum.Gift:
                    {
                        AddEventListener("gift", callback);
                    }
                    break;
            }
        }
    }
}
