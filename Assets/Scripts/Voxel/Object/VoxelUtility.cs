using System.Collections.Generic;
using UnityEngine;

namespace govox
{
    public class VoxelUtility
    {
        public static IVoxel Create(IVoxel prefab, Transform parent)
        {
            if(_voxelMap == null)
            {
                _voxelMap = new Dictionary<int, IVoxel>();
            }

            var instance = GameObject.Instantiate((MonoBehaviour)prefab, parent);
            var id = instance.GetInstanceID();
            var voxel = instance as IVoxel;
            _voxelMap.Add(id, voxel);

            return voxel;
        }

        public static void Destroy()
        {
            
        }

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
    }
}