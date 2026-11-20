using BBEngine.Math;
using Raylib_cs;
using System.Runtime.Serialization;

namespace BBEngine.Level.Objs
{
    [DataContract]
    [KnownType(typeof(vec2))]
    [KnownType(typeof(transform))]
    [KnownType(typeof(Color))]
    [KnownType(typeof(string))]
    public class Sprite : LevelObject
    {
        public Sprite(string name, string imageKey) : base(name)
        {
            image_key = imageKey;
        }

        /// <summary>
        /// Target image key to display
        /// </summary>
        [DataMember]
        public string image_key { get; set; }

        [DataMember]
        public Color tint { get; set; } = Color.White;

        [DataMember]
        public vec2 sprite_offset { get; set; } = vec2.zero;
    }
}
