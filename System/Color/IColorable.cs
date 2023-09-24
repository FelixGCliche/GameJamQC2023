using System.Collections.Generic;

namespace GameJamQC2023.Color
{
    public interface IColorable
    {
        public Queue<Godot.Color> HeldColors { get; }
        public Godot.Color GetBlendedColor();

        public Godot.Color EnqueueColor(Godot.Color newColor);
    }
}
