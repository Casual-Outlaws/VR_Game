Shader "Custom/WorldSpaceRippleShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_RippleOrigin("Ripple Origin", Vector) = (0, 0, 0, 0)
		_RippleDistance("Ripple Distance", Float) = 0.0
		_RippleWidth("Ripple Width", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _RippleOrigin;
		float _RippleDistance;
		float _RippleWidth;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			float distance = length( IN.worldPos.xyz - _RippleOrigin.xyz ) - _RippleDistance * _RippleOrigin.w;
			float halfWidth = _RippleWidth * 0.5;
			float lowerDistance = distance - halfWidth;
			float upperDistance = distance + halfWidth;

            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//clip( c.a - 0.5 );
			fixed4 c = fixed4( pow( 1 - ( abs(distance) / halfWidth ), 8 ), 0, 0, 1 );
			float ringStrength = pow( 1 - ( abs( distance ) / halfWidth ), 8 ) * ( lowerDistance < 0 && upperDistance > 0 );
			o.Albedo = ringStrength * c.rgb *( 1 - _RippleOrigin.w );
			o.Emission = o.Albedo;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
