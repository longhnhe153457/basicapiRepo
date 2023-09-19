namespace APISecurityBasic.Models.DTO
{
    public class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO> { 
        new VillaDTO { Id = 1,Name = "Pool View",Sqft=100, Occupany=4},
        new VillaDTO { Id = 2,Name = "Beach View",Sqft=300, Occupany=3}
        };
    }
}
