﻿using Godot;

namespace GameJamQC2023.Utils
{
    public static class ColorExtensions
    {
        public static Godot.Color RgbBlend(this Godot.Color baseColor, Godot.Color other)
        {
            var blendR = Mathf.Clamp(baseColor.R + other.R, 0f, 1f);
            var blendG = Mathf.Clamp(baseColor.G + other.G, 0f, 1f);
            var blendB = Mathf.Clamp(baseColor.B + other.B, 0f, 1f);

            var blend = new Godot.Color(blendR, blendG, blendB);
            if(blend == Colors.White)
                blend = Colors.Black;
            return blend;
        }
    }
}