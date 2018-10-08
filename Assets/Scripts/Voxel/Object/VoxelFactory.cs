using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using govox;

public class VoxelFactory
{
	    public static IVoxel Create<T>(T prefab, Transform parent) where T : MonoBehaviour, IVoxel
        {
            var instance = GameObject.Instantiate(prefab, parent);
            var id = instance.GetInstanceID();
            VoxelUtility.VoxelMap.Add(id, instance);

            return instance;
        }

        public static bool TryGetVoxel(int id, out IVoxel voxel)
        {
            return VoxelUtility.VoxelMap.TryGetValue(id, out voxel);
        }

        public static void Destroy(GameObject instance)
        {
            if(VoxelUtility.VoxelMap.ContainsKey(instance.GetInstanceID()))
            {
                VoxelUtility.VoxelMap.Remove(instance.GetInstanceID());
            }

            Destroy(instance);
        }
}
