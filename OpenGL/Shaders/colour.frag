#version 320 es

precision highp float;

in vec4 vertColour;

out vec4 outColour;

void main()
{
    outColour = vertColour;
}
