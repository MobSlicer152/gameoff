#version 320 es

precision highp float;

in vec2 texCoord;

out vec4 outColour;

uniform sampler2D inTexture;

void main()
{
    outColour = texture(inTexture, texCoord);
}
