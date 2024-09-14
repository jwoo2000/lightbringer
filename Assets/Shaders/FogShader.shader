// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "_Shaders/FogShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }

        Pass
        {
            CGPROGRAM
            #pragma exclude_renderers ps4 ps5 xboxone xboxseries switch
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct vertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct fragmentInput
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fragmentInput vert (vertexInput v)
            {
                fragmentInput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            half4 frag (fragmentInput i) : COLOR
            {

                return float4(0.0f, 0.0f, 0.0f, 0.0f);
            }
            ENDCG
        } // End Pass
    } // End SubShader

    FallBack "Diffuse"
}
