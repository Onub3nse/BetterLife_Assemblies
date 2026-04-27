using Mafi.Base;
using TerrainID = Mafi.Core.Terrain.Surfaces.TerrainTileSurfaceDecalProto.ID;
using Mafi.Core.Terrain;

namespace BetterLife;

public partial class BetterLIDs
{
    public partial class Surfaces
    {
        public static readonly TerrainID speed1l = Ids.TerrainTileSurfaces.CreateId("speed1l");
        public static readonly TerrainID speed1r = Ids.TerrainTileSurfaces.CreateId("speed1r");
        public static readonly TerrainID speed1n = Ids.TerrainTileSurfaces.CreateId("speed1n");
        public static TerrainTileSurfaceProto surface1l;
        public static TerrainTileSurfaceProto surface1r;
        public static TerrainTileSurfaceProto surface1n;

    }
}