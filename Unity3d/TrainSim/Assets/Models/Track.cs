

using UnityEngine;

namespace Assets.Models
{
    public class Track
    {
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public string TrackType { get; set; }

        public Track(Vector3 startPosition, Vector3 endPosition, string trackType)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            TrackType = trackType;
        }
    }
}
