Shader "_Shaders/BasicSolidColourShader"
{
    Properties
    {
        _Color ("Colour", Color) = (1.0,0.6,0.8,1.0)
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }

        Pass
        {
            CGPROGRAM
            #pragma exclude_renderers ps4 ps5 xboxone xboxseries switch
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform float4 _Color;

            struct vertexInput
            {
                float4 vertex : POSITION;
            };

            struct fragmentInput
            {
                float4 vertex : SV_POSITION;
            };


            fragmentInput vert (vertexInput v)
            {
                fragmentInput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (fragmentInput i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
