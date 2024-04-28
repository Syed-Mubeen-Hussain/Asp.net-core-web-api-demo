using Magic_VillaApi.Models.DTO;
using System.Xml.Linq;

namespace Magic_VillaApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>()
            {
                new VillaDTO() { Id = 1,Name = "Park Villa",Occpancy = "1000",Sqrft = "10"},
                new VillaDTO() { Id = 2,Name = "Townhouse Villa",Occpancy = "2000",Sqrft = "20"},
            };
    }
}
