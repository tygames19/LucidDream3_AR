﻿Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex ("NoiseTex (RGB) ", 2D) = "white" {}
		_Cut ("Cutout", Range(0,5)) = 0
		_OutThickness ("OutThickness", Range (1,2)) = 1.15
		[HDR]_OutColor ("OutColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		//1st pass zwrite on, Rendering off
	   zwrite on
	   ColorMask 0

	   CGPROGRAM
	   #pragma surface surf nolight noambient nolightmap noshadow 

	   struct Input
	   {
		   float4 color:COLOR;
	   };

	   void surf(Input IN, inout SurfaceOutput o)
	   {

	   }

	   float4 Lightingnolight(SurfaceOutput s, float3 LightDir, float atten)
	   {
		   return float4 (0, 0, 0, 0);
	   }

	   ENDCG

		   //2nd pass zwrite off, Rendering on

		zwrite off
		CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        
		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _Cut;
		float _OutThickness;
		float4 _OutColor;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_NoiseTex; 
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 noise = tex2D (_NoiseTex, IN.uv_NoiseTex);
            o.Albedo = c.rgb;

			float alpha;
			if (noise.r >= _Cut)
			alpha = 1;
			else
			alpha = 0;

			float outline;
			if (noise.r >= _Cut * _OutThickness)
			outline = 0;
			else
			outline = 1;

			o.Emission = outline * _OutColor.rgb;
            o.Alpha = alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
