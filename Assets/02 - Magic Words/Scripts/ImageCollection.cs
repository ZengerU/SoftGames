using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MagicWords
{
    internal class ImageCollection<T> : KeyedCollection<string, T> where T : Image
    {
        protected override string GetKeyForItem(T img) => img.Name;

        internal void AddRange(List<T> images)
        {
            foreach (var img in images)
            {
                Add(img);
            }
        }
    }
}