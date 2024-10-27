Shader "_Shaders/FogTest"
{
    Properties
    {
        _OldTex ("Blend From Texture", 2D) = "black" {}
        _CurrTex ("Blend To Texture", 2D) = "black" {}
        _Color ("Fog Colour", Color) = (0,0,0,1)
        _FogWallHeight ("Fog Wall Height", Float) = 20.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent+150" }
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
            uniform sampler2D _OldTex;
            uniform sampler2D _CurrTex;
            uniform fixed4 _Color;
            uniform float _FogWallHeight;
            uniform float _Blend;

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

                // if a > 0.5, translate vertex down in y axis by fogwallheight
                // "make fog droop down at visible edges"
                float texColorAlpha = tex2Dlod(_OldTex, v.texcoord).a;
                v.vertex.y -= step(0.5, texColorAlpha)*_FogWallHeight;
                
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (fragmentInput i) : SV_Target
            {
                float rOld = tex2D(_OldTex, i.uv).a;
                float rCurr = tex2D(_CurrTex, i.uv).a;

                fixed a = lerp(rOld, rCurr, _Blend);

                _Color.a = max(0, _Color.a - a);
                return _Color;
            }
            ENDCG
        }
    }
}
