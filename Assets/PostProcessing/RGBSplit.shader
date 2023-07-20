Shader "Unlit/RGBSplit"
{
    HLSLINCLUDE
    #include "../PostProcessing/Shaders/XPostProcessing.hlsl"
// half可以具有任何值，而此处声明二维向量(元素为标量)
    uniform half2 _Params;
    #define _Indensity _Params.x;
    #define _TimeX _Params.y;
    float randomNoise(float x, float y){
        return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 43758.5453);
    }
    ENDHLSL
    SubShader{
        Cull Off ZWrite Off ZTest Always
        Pass{
            HLSLPROGRAM
            ENDHLSL
        }
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
