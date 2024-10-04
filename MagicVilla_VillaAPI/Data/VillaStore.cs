﻿using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI.Data
{
    public class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
            {
                new VillaDTO {Id=1,Name="Pool view",Sqft=100,Occupancy=2 },
                new VillaDTO {Id=2,Name="Beach view",Sqft=300,Occupancy=3 },
            };
    }
}
