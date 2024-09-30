Shader "_Shaders/UnexploredFogShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "black" {}
        _Color ("Fog Colour", Color) = (0,0,0,0)
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

            struct vertexInput
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct fragmentInput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };


            fragmentInput vert (vertexInput v)
            {
                fragmentInput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
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
