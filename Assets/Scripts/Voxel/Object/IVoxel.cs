using UnityEngine;
using UdonLib.Commons;

namespace govox
{
    public interface IVoxel : IRayTriggerHandler
    {
         VoxelHitInfo GetHitInfo(RaycastHit hit);
    }
}