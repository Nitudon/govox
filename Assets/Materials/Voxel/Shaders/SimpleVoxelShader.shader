Shader "Voxel/SimpleVoxelShader" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_DirectionalColor ("Directional Color", Color) = (1,0,0,1)

		[KeywordEnum(None, Up, Down, Forward, Back, Left, Right)] _Direction ("Hit Direction", Int) = 0
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			Cull Back

			CGPROGRAM
			#pragma vertex vert
    		#pragma fragment frag

			#include "UnityCG.cginc"

			#pragma multi_compile _DIRECTION_NONE, _DIRECTION_LEFT _DIRECTION_RIGHT _DIRECTION_UP _DIRECTION_DOWN _DIRECTION_FORWARD _DIRECTION_BACK

			float4 _Color;
			float4 _DirectionalColor;

			struct appdata
            {
                float4 vertex : POSITION;
				half3 normal : NORMAL;
            };


        	struct v2f
        	{
				float4 vertex :SV_POSITION;
				half3 normal : NORMAL;
        	};

			v2f vert (appdata v)
        	{
        	    v2f o;
        	    o.vertex = UnityObjectToClipPos(v.vertex);
        	    o.normal = UnityObjectToWorldNormal(v.normal);
        	    return o;
        	}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = _Color;
				fixed4 dirC = _DirectionalColor;

				#ifdef _DIRECTION_NONE
					return c;
				#elif _DIRECTION_LEFT
					return i.normal.x < 0 ? dirC : c; 
				#elif _DIRECTION_RIGHT
					return i.normal.x > 0 ? dirC : c; 
				#elif _DIRECTION_UP
					return i.normal.y > 0 ? dirC : c; 
				#elif _DIRECTION_DOWN
					return i.normal.y < 0 ? dirC : c; 
				#elif _DIRECTION_FORWARD
					return i.normal.z > 0 ? dirC : c; 
				#elif _DIRECTION_BACK
					return i.normal.z < 0 ? dirC : c; 
				#endif

				return c;
			}
			ENDCG
		}
	}
}