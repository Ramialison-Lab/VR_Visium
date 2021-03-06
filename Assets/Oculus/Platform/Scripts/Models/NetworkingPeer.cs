namespace Oculus.Platform.Models
{
    using System;

    public class NetworkingPeer
    {
        public NetworkingPeer(UInt64 id, PeerConnectionState state)
        {
            ID = id;
            State = state;
        }

        public UInt64 ID { get; private set; }
        public PeerConnectionState State { get; private set; }
    }
}
