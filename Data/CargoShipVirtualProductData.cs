using Mafi;
using Mafi.Core.Mods;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;

namespace IndustrialExpansion.Data
{
    internal class CargoShipVirtualProductData : IModData
    {
        public void RegisterData(ProtoRegistrator reg)
        {
            Log.Info("[IndustrialExpansion] CargoShipVirtualProductData : Registering Cargo Ship Virtual Product Data prototype.");

            ProductProto.Gfx gfx = new ProductProto.Gfx(Option<string>.None, "Assets/Unity/UserInterface/WorldMap/CargoShipStoryIcon256.png", ColorRgba.Gold, default(ColorRgba), default(ColorRgba));
            Proto.Str str = Proto.CreateStr(ModIds.Products.CargoShipsQueue, ModTranslation.Get("Cargo Ship"), ModTranslation.Get("The amount of cargo ships that have been built but not deployed."), null);
            VirtualProductProto virtualProductProto = new VirtualProductProto(ModIds.Products.CargoShipsQueue, str, gfx, false, true, null, null);
            reg.PrototypesDb.Add<VirtualProductProto>(virtualProductProto, false);

            Log.Info("[IndustrialExpansion] CargoShipVirtualProductData : Cargo Ship Virtual Product Data prototype registered.");

        }
    }
}