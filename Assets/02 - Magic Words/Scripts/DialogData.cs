using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

namespace MagicWords
{
    [Serializable]
    public class DialogData
    {
        [JsonProperty("dialogue")] public List<DialogueEntry> Dialogue { get; set; }
        [JsonProperty("emojies")] public List<Emoji> Emojis { get; set; }
        [JsonProperty("avatars")] public List<Avatar> Avatars { get; set; }
    }

    public class DialogueEntry
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
    }

    public abstract class Image
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("texture")] public Texture2D Texture { get; set; }
        [JsonProperty("url")] public string URL { get; set; }
    }

    public class Emoji : Image
    {
        public Sprite Sprite;
        public uint Index;
    }

    public class Avatar : Image
    {
        public AvatarPosition AvatarPosition { get; set; }
        [JsonProperty("position")] public string Position { get; set; }

        [OnSerialized]
        internal void SetupPosition(StreamingContext context)
        {
            AvatarPosition = Position == "left" ? AvatarPosition.Left : AvatarPosition.Right;
        }
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            AvatarPosition = Position == "left" ? AvatarPosition.Left : AvatarPosition.Right;
        }
    }

    public enum AvatarPosition
    {
        Left,
        Right
    }
}