using System.Collections.Generic;
using UnityEngine;
using UdonLib.Commons;

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

        public static Vector3 DirectionalCenterPoint(Vector3 anchor, DirectionXYZ direction)
        {
            switch(direction)
            {
                case DirectionXYZ.Up :
                    return anchor + Vector3.up * ObjectDefine.UNIT_VOXEL_SCALE * 0.5f;
                case DirectionXYZ.Down :
                    return anchor + Vector3.down * ObjectDefine.UNIT_VOXEL_SCALE * 0.5f;
                case DirectionXYZ.Front :
                    return anchor + Vector3.forward * ObjectDefine.UNIT_VOXEL_SCALE * 0.5f;
                case DirectionXYZ.Back :
                    return anchor + Vector3.back * ObjectDefine.UNIT_VOXEL_SCALE * 0.5f;
                case DirectionXYZ.Left :
                    return anchor + Vector3.left * ObjectDefine.UNIT_VOXEL_SCALE * 0.5f;
                case DirectionXYZ.Right :
                    return anchor + Vector3.right * ObjectDefine.UNIT_VOXEL_SCALE * 0.5f;
                default :
                    return anchor;
            }
        }
    }
}