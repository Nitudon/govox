public static class ShaderUtility
{
	public static string GetKeywordForMeshDirection(UdonLib.Commons.DirectionXYZ dir)
	{
		string upperDir = System.Enum.GetName(typeof(UdonLib.Commons.DirectionXYZ), dir).ToUpper();
		return $"_DIRECTION_{upperDir}";
	}
}
