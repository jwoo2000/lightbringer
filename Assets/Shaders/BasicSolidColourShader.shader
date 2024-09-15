Shader "_Shaders/BasicSolidColourShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
                return fixed4(1.0f,0.6f,0.8f,1.0f);
            }
            ENDCG
        }
    }
}
