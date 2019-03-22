Shader "Custom/Standard World Tiled"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
		_TileSize ("Tile Size", Vector) = (1.0, 1.0, 0.0 )
		_Offset ("Offset", Vector) = ( 0.0, 0.0, 0.0 )
		_TextureContrast ("Texture Contrast", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos : TEXCOORD0;
        };

        half _Glossiness;
		half _TextureContrast;
        fixed4 _Color;
		fixed3 _TileSize;
		fixed3 _Offset;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			float2 plane = float2(IN.worldPos.x * _TileSize.x, IN.worldPos.z * _TileSize.y);
			float2 offset = float2(_Offset.x, _Offset.y);

            fixed4 c = clamp(tex2D (_MainTex, plane + offset) + _TextureContrast, 0, 1) * _Color;
            o.Albedo = c.rgb;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
