using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using govox;

public class VoxelFactory
{
		private static Dictionary<int, IVoxel> _voxelMap;

	    public static IVoxel Create<T>(T prefab, Transform parent) where T : MonoBehaviour, IVoxel
        {
            if(_voxelMap == null)
            {
                _voxelMap = new Dictionary<int, IVoxel>();
            }

            var instance = GameObject.Instantiate(prefab, parent);
            var id = instance.GetInstanceID();
            _voxelMap.Add(id, instance);

            return instance;
        }

        public static bool TryGetVoxel(int id, out IVoxel voxel)
        {
            if(_voxelMap == null)
            {
                voxel = default(IVoxel);
                return false;
            }
            
            return _voxelMap.TryGetValue(id, out voxel);
        }

        public static void Destroy(GameObject instance)
        {
            if(_voxelMap != null && _voxelMap.ContainsKey(instance.GetInstanceID()))
            {
                _voxelMap.Remove(instance.GetInstanceID());
            }

            Destroy(instance);
        }
}
