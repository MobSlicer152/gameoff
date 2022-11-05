#version 320 es

layout (location = 0) in vec3 inPos;
layout (location = 1) in vec4 inColour;
layout (location = 2) in vec2 inTexCoord;

out vec4 vertColour;
out vec2 texCoord;

void main()
{
    gl_Position = vec4(inPos, 1);
    vertColour = inColour;
    texCoord = inTexCoord;
}
