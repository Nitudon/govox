using UnityEngine;
using UdonLib.Commons;

namespace govox
{
    public struct VoxelHitInfo
    {
        public readonly IVoxel VoxelInfo;

        public readonly Vector3 HitPosition;

        public readonly Vector3 MeshCenter;

        public readonly DirectionXYZ NormalDirection;

        public VoxelHitInfo(IVoxel voxel, Vector3 hitPosition, Vector3 center, DirectionXYZ normal)
        {
            VoxelInfo = voxel;
            HitPosition = hitPosition;
            MeshCenter = center;
            NormalDirection = normal;
        }
    }
}