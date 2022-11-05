#version 320 es

layout (location = 0) in vec3 inPos;
layout (location = 1) in vec4 inColour;

out vec4 vertColour;

void main()
{
    gl_Position = vec4(inPos, 1);
    vertColour = inColour;
}
