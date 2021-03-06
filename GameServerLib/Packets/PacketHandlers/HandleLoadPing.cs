﻿using ENet;
using GameServerCore.Packets.Enums;
using LeagueSandbox.GameServer.Players;

namespace LeagueSandbox.GameServer.Packets.PacketHandlers
{
    public class HandleLoadPing : PacketHandlerBase
    {
        private readonly Game _game;
        private readonly PlayerManager _playerManager;

        public override PacketCmd PacketType => PacketCmd.PKT_C2S_PING_LOAD_INFO;
        public override Channel PacketChannel => Channel.CHL_C2S;

        public HandleLoadPing(Game game)
        {
            _game = game;
            _playerManager = game.PlayerManager;
        }

        public override bool HandlePacket(Peer peer, byte[] data)
        {
            var request = _game.PacketReader.ReadPingLoadInfoRequest(data);
            var peerInfo = _playerManager.GetPeerInfo(peer);
            if (peerInfo == null)
            {
                return false;
            }

             _game.PacketNotifier.NotifyPingLoadInfo(request, peerInfo.UserId);
            return true;
        }
    }
}
