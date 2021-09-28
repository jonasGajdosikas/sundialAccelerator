using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace sundialAccelerator{

    public class ConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Label("Maximum Banking")]
        [Tooltip("Maximum amount of days you can bank for accelerated crafting")]
        [DefaultValue(3)]
		public int maxBanking{get;set;}

        [Label("Tree recipes enabled")]
        [Tooltip("Enable crafting wood from acorns where they would be grown")]
        [DefaultValue(true)]
        public bool treeRecipesEnabled { get; set; }

        [Label("Herb recipes enabled")]
        [Tooltip("Enable crafting herbs when and where they bloom")]
        [DefaultValue(true)]
        public bool herbRecipesEnabled { get; set; }

        [Label("Natural generated recipes enabled")]
        [Tooltip("Enable crafting naturally generating resources")]
        [DefaultValue(true)]
        public bool natRecipesEnabled { get; set; }

        [Label("Chlorophyte acceleration enabled")]
        [Tooltip("Enable crafting chlorophyte and mud into more chlorophyte")]
        [DefaultValue(true)]
        public bool chlorophyteEnabled { get; set; }

        [Label("Days consumed by crafting chlorophyte")]
        [DefaultValue(2)]
        public int chlorophyteDaysConsumed { get; set; }
    }
}