using System;

[Flags]
public enum CameraTypeModifier
{
    None = 0,
    Ice = 1,
    Warm = 2,
    Teleport = 4,
    Springs = 8,
    Platform = 16
}
