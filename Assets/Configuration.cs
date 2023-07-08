using UnityEngine;

public static class Configuration
{
    public static class Input
    {
        
    }

    public static class Tags
    {
        public const string Water = "Water";
    }

    public static class LayerMasks
    {
        public static readonly LayerMask Ground = LayerMask.GetMask("Ground");
        public static LayerMask Water = LayerMask.GetMask("Water");
        public static LayerMask Ice = LayerMask.GetMask("Ice");
    }

    public static class Layers
    {
        public static int Water = LayerMask.NameToLayer("Water");
        public static int Ice = LayerMask.NameToLayer("Ice");
    }
}
