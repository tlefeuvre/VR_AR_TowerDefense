Shader "Unlit/ToonShader"
{
    Properties
    {
        _Albedo ("Albedo", Color) = (1,1,1,1)
        _Shades ("Shades",Range(1,20)) = 3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

       
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal: NORMAL;
               
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            
            float4 _Albedo;
            float _Shades;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
               o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
               float cosineAngle = dot(normalize(i.worldNormal),normalize(_WorldSpaceLightPos0.xyz));
               cosineAngle = max(cosineAngle,0.0);
               cosineAngle = floor(cosineAngle * _Shades)/_Shades;
                return _Albedo * cosineAngle;
            }
            ENDCG
        }
    }
}
