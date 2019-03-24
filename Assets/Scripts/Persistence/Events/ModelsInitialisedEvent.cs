using Curveball;

namespace LastStand
{
    public struct ModelsInitialisedEvent : IEvent
    {
        public bool FromSaveFile;

        public ModelsInitialisedEvent(bool fromSaveFile)
        {
            FromSaveFile = fromSaveFile;
        }
    }
}