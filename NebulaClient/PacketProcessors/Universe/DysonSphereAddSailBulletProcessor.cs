﻿using NebulaModel.Networking;
using NebulaModel.Packets.Universe;
using NebulaModel.Logger;
using NebulaModel.DataStructures;
using NebulaModel.Packets.Processors;
using NebulaModel.Attributes;

namespace NebulaClient.PacketProcessors.Universe
{
    [RegisterPacketProcessor]
    class DysonSphereAddSailBulletProcessor : IPacketProcessor<DysonSphereBulletCorrectionPacket>
    {
        public void ProcessPacket(DysonSphereBulletCorrectionPacket packet, NebulaConnection conn)
        {
            Log.Info($"Processing authorization/correction packet for bullet (ID: {packet.BulletId})");
            //Check if the bullet that needs to be corrected exists
            if (GameMain.data.dysonSpheres[packet.StarIndex]?.swarm?.bulletPool[packet.BulletId] != null)
            {
                //Update destination values for the bullet
                SailBullet bullet = GameMain.data.dysonSpheres[packet.StarIndex].swarm.bulletPool[packet.BulletId];
                bullet.uEnd = DataStructureExtensions.ToUnity(packet.UEnd);
                bullet.uEndVel = DataStructureExtensions.ToUnity(packet.UEndVel);
            } else
            {
                //TODO: Maybe queue it and check next frame if the bullet already exist?
                //Note: this situation was not observed during test, but maybe it can due to the severe lags?
            }
        }
    }
}