Shader "Custom/2DOutlineKeepEdgesWithColor"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineThickness ("Outline Thickness", Float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            // Draw the outline
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float _OutlineThickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the main texture
                fixed4 texColor = tex2D(_MainTex, i.uv) * i.color;

                // Sample neighboring pixels
                float2 offsets[8] = {
                    float2(_OutlineThickness, 0), float2(-_OutlineThickness, 0),
                    float2(0, _OutlineThickness), float2(0, -_OutlineThickness),
                    float2(_OutlineThickness, _OutlineThickness), float2(-_OutlineThickness, -_OutlineThickness),
                    float2(_OutlineThickness, -_OutlineThickness), float2(-_OutlineThickness, _OutlineThickness)
                };

                // Determine if the current pixel is on the edge
                float isEdge = 0.0;
                for (int j = 0; j < 8; j++)
                {
                    fixed4 neighbor = tex2D(_MainTex, i.uv + offsets[j]);
                    if (neighbor.a < 0.1) // Adjust threshold if needed
                    {
                        isEdge = 1.0;
                    }
                }

                // Return the original color if it's on the edge, otherwise return fully transparent
                return isEdge > 0.0 ? texColor : fixed4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
