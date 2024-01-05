using System.Numerics;
namespace AltV.Atlas.Raycast.Client.Models;

/// <summary>
/// A class that stores all information about a raycast
/// </summary>
public class RaycastHitInfo
{
    /// <summary>
    /// The raycast handle
    /// </summary>
    public int Handle = -1;
    
    /// <summary>
    /// Did the raycast hit
    /// </summary>
    public bool Hit = false;

    /// <summary>
    /// The position of the raycast hit
    /// </summary>
    public Vector3 Position = new( );

    /// <summary>
    /// The surface normal positon
    /// </summary>
    public Vector3 SurfaceNormal = new( );
    
    /// <summary>
    /// The hit material hash
    /// </summary>
    public uint MaterialHash = 0;
    
    /// <summary>
    /// The entity hit
    /// </summary>
    public uint EntityHit = 0;
}