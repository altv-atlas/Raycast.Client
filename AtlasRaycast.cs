using AltV.Atlas.Raycast.Client.Enums;
using AltV.Atlas.Raycast.Client.Models;
using AltV.Atlas.Shared;
using AltV.Atlas.Shared.Extensions;
using AltV.Net.Client;
using AltV.Net.Data;
namespace AltV.Atlas.Raycast.Client;

/// <summary>
/// The atlas class that provides useful raycast methods
/// </summary>
public static class AtlasRaycast
{
    /// <summary>
    /// Executes a raycast to the point the player is looking at
    /// </summary>
    /// <param name="radius">The radius of the raycast</param>
    /// <returns>Returns the information about the hit entity if hit</returns>
    public static RaycastHitInfo GetAimingPoint( int radius = 1 )
    {
        return RaycastCalculation( radius, ERaycastType.Point );
    }

    /// <summary>
    /// Executes a raycast to the point the player is looking at in a capsule
    /// </summary>
    /// <param name="radius">The radius of the raycast</param>
    /// <returns>Returns the information about the hit entity if hit</returns>
    public static RaycastHitInfo GetAimingPointCapsule( int radius = 1 )
    {
        return RaycastCalculation( radius, ERaycastType.Capsule );
    }

    private static RaycastHitInfo RaycastCalculation( int radius, ERaycastType type )
    {
        var start = Alt.Natives.GetFinalRenderedCamCoord( );
        var forwardVector = Alt.Natives.GetFinalRenderedCamRot( 2 ).RotAnglesToVector( );
        var frontOf = new Position
        {
            X = ( start.X + ( forwardVector.X * 2000 ) ),
            Y = ( start.Y + ( forwardVector.Y * 2000 ) ),
            Z = ( start.Z + ( forwardVector.Z * 2000 ) )
        };

        var raycast = type switch
        {
            ERaycastType.Point => Alt.Natives.StartExpensiveSynchronousShapeTestLosProbe( start.X, start.Y, start.Z, frontOf.X, frontOf.Y, frontOf.Z, -1, Alt.LocalPlayer, 7 ),
            ERaycastType.Capsule => Alt.Natives.StartShapeTestCapsule( start.X, start.Y, start.Z, frontOf.X, frontOf.Y, frontOf.Z, radius, -1, Alt.LocalPlayer, 7 ),
            _ => throw new ArgumentOutOfRangeException( nameof( type ), type, null )
        };

        var ray = new RaycastHitInfo( );

        ray.Handle = Alt.Natives.GetShapeTestResultIncludingMaterial( raycast, ref ray.Hit, ref ray.Position, ref ray.SurfaceNormal, ref ray.MaterialHash, ref ray.EntityHit );

        DevTools.LogFields( ray );

        return ray;
    }

}