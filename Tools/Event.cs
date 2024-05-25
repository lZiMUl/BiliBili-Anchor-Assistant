namespace BiliBili_Anchor_Assistant.Tools
{
    public class Event<T>
    {
        private readonly Dictionary<string, List<Action<T>>> _eventListeners = new();

        protected void AddEventListener(string eventName, Action<T> callback)
        {
            if (!_eventListeners.ContainsKey(eventName))
            {
                _eventListeners[eventName] = new List<Action<T>>();
            }
            _eventListeners[eventName].Add(callback);
        }

        protected void Emit(string eventName, T args)
        {
            foreach (var callback in _eventListeners[eventName])
            {
                callback.Invoke(args);
            }
        }
    }
}
