Shader "Custom/Flowmaps"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
       [noscaleoffset] _FlowTexture ("Flow Texture With (A) Time Offset", 2D) = "black" {}
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
            sampler2D _FlowTexture;

            float3 flowUV(float2 uv, float2 flowDirection, float time, float offset) {
                float progress = frac(time + offset);
                // uv value + weight function to mix 2 uv mapping
                float3 uvw;
                uvw.xy = uv - flowDirection * progress;  
                uvw.z = 1 - abs(1 - 2 * progress);
                return uvw;
            }

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
                // sample flowmap, and normalize
                float2 flowDirection = tex2D(_FlowTexture, i.uv).rg * 2 - 1;
                // sample the alpha of the flowmap that stores a noise
                float noise = tex2D(_FlowTexture, i.uv).a;
                float time = _Time.y + noise;

                // compute uv distortion and color weights based on flow data
                // sample it twice with offseted phase, to create alternating flows
                float3 uvwBase  = flowUV(i.uv, flowDirection, time, 0.5);
                float3 uvwOffset  = flowUV(i.uv, flowDirection, time, 0);


                // sample the main texture based on uv, but go towards black based on weight  
                float4 flowColor = tex2D(_MainTex, uvwBase.xy) * uvwBase.z;
                float4 otherFlowColor = tex2D(_MainTex, uvwOffset.xy) * uvwOffset.z;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return flowColor + otherFlowColor;
            }
            ENDCG
        }
    }
}
