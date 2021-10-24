using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace sundialAccelerator{

    public class ConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Maximum Banking")]
        [Tooltip("Maximum amount of days you can bank for accelerated crafting")]
        [DefaultValue(3)]
		public int MaxBanking{get;set;}

        [Label("Tree recipes enabled")]
        [Tooltip("Enable crafting wood from acorns where they would be grown\nRequires reload to take effect")]
        [DefaultValue(true)]
        public bool TreeRecipesEnabled { get; set; }

        [Label("Amount of wood on craft")]
        [DefaultValue(15)]
        public int AmountWoodOnCraft { get; set; }

        [Label("Herb recipes enabled")]
        [Tooltip("Enable crafting herbs when and where they bloom\nRequires reload to take effect")]
        [DefaultValue(true)]
        public bool HerbRecipesEnabled { get; set; }

        [Label("Amount of herbs per craft")]
        [Tooltip("Adjust the amount of herbs you get each time you accelerate time")]
        [DefaultValue(6)]
        public int AmountHerbsOnCraft { get; set; }

        [Label("Natural generated recipes enabled")]
        [Tooltip("Enable crafting naturally generating resources\nRequires reload to take effect")]
        [DefaultValue(true)]
        public bool NatRecipesEnabled { get; set; }

        [Label("Chlorophyte acceleration enabled")]
        [Tooltip("Enable crafting mud into chlorophyte\nRequires chlorophyte in inventory to craft\nRequires reload to take effect")]
        [DefaultValue(true)]
        public bool ChlorophyteEnabled { get; set; }

        [Label("Days consumed by crafting chlorophyte")]
        [DefaultValue(2)]
        public int ChlorophyteDaysConsumed { get; set; }

        [Label("Amount of chlorophyte crafted")]
        [DefaultValue(6)]
        public int ChlorophyteAmountCraft { get; set; }
    }
}