sampler TextureSampler : register(s0);

float weights[5] = {0.2270270270, 0.1945945946, 0.1216216216, 0.0540540541, 0.0162162162};

float2 offsets[5] = {
    float2(0, 0),
    float2(1, 0),
    float2(-1, 0),
    float2(2, 0),
    float2(-2, 0)
};

float4 HorizontalBlur(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(TextureSampler, texCoord) * weights[0];
    for (int i = 1; i < 5; i++)
    {
        color += tex2D(TextureSampler, texCoord + offsets[i] / 800.0) * weights[i];
        color += tex2D(TextureSampler, texCoord - offsets[i] / 800.0) * weights[i];
    }
    return color;
}

float4 VerticalBlur(float2 texCoord : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(TextureSampler, texCoord) * weights[0];
    for (int i = 1; i < 5; i++)
    {
        color += tex2D(TextureSampler, texCoord + offsets[i] / 600.0) * weights[i];
        color += tex2D(TextureSampler, texCoord - offsets[i] / 600.0) * weights[i];
    }
    return color;
}

technique Blur
{
    pass P0
    {
        PixelShader = compile ps_2_0 HorizontalBlur();
    }

    pass P1
    {
        PixelShader = compile ps_2_0 VerticalBlur();
    }
}
