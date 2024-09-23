Shader "_Shaders/FogTest"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "black" {}
        _Color ("Fog Colour", Color) = (0,0,0,0)
        _FogWallHeight ("Fog Wall Height", Float) = 2.0
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
            uniform sampler2D _MainTex;
            uniform fixed4 _Color;
            uniform float _YOffset;
            uniform float _FogWallHeight;
            uniform float _FogWallDistance;

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
                float texColorAlpha = tex2Dlod(_MainTex, v.texcoord).a;

                // if alpha > 0.5, translate vertex down in y axis by fogwallheight
                v.vertex.y -= step(0.5, texColorAlpha)*_FogWallHeight;
                
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (fragmentInput i) : SV_Target
            {
                _Color.a = max(0, _Color.a - tex2D(_MainTex, i.uv).a);
                return _Color;
            }
            ENDCG
        }
    }
}
