Shader "_Shaders/FogShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "black" {}
    }
    SubShader
    {
        Tags { "Queue" = "Transparent+1" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Cull Off
            CGPROGRAM
            #pragma exclude_renderers ps4 ps5 xboxone xboxseries switch
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // uniforms
            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct vertexInput
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct fragmentInput
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fragmentInput vert (vertexInput v)
            {
                fragmentInput o;
                float4 texColor = tex2Dlod(_MainTex, v.texcoord);
                v.vertex.y += texColor.a;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

                return o;
            }

            float4 frag (fragmentInput i) : COLOR
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        } // End Pass
    } // End SubShader

    FallBack "Diffuse"
}
