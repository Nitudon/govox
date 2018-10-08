using System.Collections.Generic;
using UnityEngine;

namespace govox
{
    public class VoxelUtility
    {
        public static bool VoxelRayCast(Vector3 origin, Vector3 direction, out VoxelHitInfo info, float maxDistance = float.MaxValue)
        {
            if(_voxelMap == null)
            {
                info = default(VoxelHitInfo);
                return false;
            }

            if(Physics.Raycast(origin, direction, out var hit, maxDistance))
            {
                var voxel = hit.collider.gameObject.GetComponent<IVoxel>();
                if(voxel != null)
                {
                    voxel.OnRayHit(hit);
                    info = voxel.GetHitInfo(hit);
                    return true;
                }
            }
            
            info = default(VoxelHitInfo);
            return false;
        }

        private static Dictionary<int, IVoxel> _voxelMap;
        public static Dictionary<int, IVoxel> VoxelMap
        {
            get
            {
                if(_voxelMap == null)
                {
                    _voxelMap = new Dictionary<int, IVoxel>();
                }
                return _voxelMap;
            }
        }
    }
}