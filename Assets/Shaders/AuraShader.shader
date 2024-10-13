Shader "_Shaders/AuraShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1, 1, 1, 1)   // Base color of the cylinder
        _GlowColor ("Glow Color", Color) = (1, 0, 0, 1)  // Color of the glowing edge
        _GlowPower ("Glow Intensity", Float) = 2.0    // Controls how strong the glow is
        _MainTex ("Main Texture", 2D) = "white" {}    // Optional texture for the object
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _GlowColor;
            float _GlowPower;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            // Vertex shader
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // Transform normal to world space
                o.worldNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));

                // Get world position of the vertex
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                
                return o;
            }

            // Fragment shader
            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the base texture
                fixed4 baseColor = tex2D(_MainTex, i.uv) * _Color;

                // Calculate view direction (from the camera to the pixel)
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);

                // Calculate the Fresnel term based on the angle between view direction and surface normal
                float fresnel = pow(1.0 - dot(i.worldNormal, viewDir), _GlowPower);

                // Combine base color and the glow effect
                return baseColor + fresnel * _GlowColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
