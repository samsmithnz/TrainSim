using System.Numerics;

namespace TrainSim;

public class Track
{
    public Vector3 StartPosition { get; set; }
    public Vector3 EndPosition { get; set; }
    public string TrackType { get; set; }
    public int Rotation { get; set; }

    public Track(Vector3 startPosition, Vector3 endPosition, string trackType, int rotation)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
        TrackType = trackType;
        Rotation = rotation;
    }
}
