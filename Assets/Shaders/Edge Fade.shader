Shader "Custom/Edge Fade"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_Edge ("Edge", Range(3.0, 20.0)) = 3.0
		_Fade ("Fade", Range(0.1, 3.0)) = 1.0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        fixed4 _Color;
		fixed _Edge;
		fixed _Fade;

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed boxRadius = max(abs(IN.worldPos.x), abs(IN.worldPos.z));
			fixed diff = boxRadius - _Edge;
			clip(diff);

            o.Albedo = _Color.rgb;
            o.Alpha = clamp(diff * (1 / _Fade), 0.0, 1.0);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
