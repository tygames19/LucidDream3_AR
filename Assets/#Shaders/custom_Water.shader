Shader "Custom/custom_Water"
{
    Properties
    {
        _BumpMap("NormalMap", 2D) = "bump" {}
		_Cube("cubeMap", Cube) = ""{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _BumpMap;
     	samplerCUBE _Cube;

        struct Input
        {
            float2 uv_BumpMap;
			float3 worldRefl;
			float3 viewDir;
			INTERNAL_DATA
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
			// water moving
			float3 normal1 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap+_Time.x * 0.1));
			float3 normal2 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap-_Time.x * 0.1));
			o.Normal = (normal1 + normal2) / 2;

			float3 refcolor = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal));
			
			//rim term
			float rim = saturate(dot(o.Normal, IN.viewDir));
			rim = pow(1 - rim, 1.5);
			
			o.Emission = refcolor * rim * 2;
			o.Alpha = saturate(rim+0.5);
        }
        ENDCG
    }
    FallBack "Legacy Shaders/Transparent/Vertexlit"
}
