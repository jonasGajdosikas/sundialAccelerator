using System;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace sundialAccelerator
{

	public class AcceleratedRecipe : ModRecipe
    {
        public AcceleratedRecipe(Mod mod) : base(mod) {}

        public override void OnCraft(Item item)
        {
            Main.sundialCooldown++;
        }
    }

	public class ChlorophyteAcceleration : AcceleratedRecipe
	{
		public ChlorophyteAcceleration(Mod mod) : base(mod) {}

		public override bool RecipeAvailable()
		{
			if (!ModContent.GetInstance<ConfigServer>().ChlorophyteEnabled) return false;
			return (Main.sundialCooldown <= ModContent.GetInstance<ConfigServer>().MaxBanking);
		}

		public override void OnCraft(Item item)
		{
			Main.sundialCooldown += ModContent.GetInstance<ConfigServer>().ChlorophyteDaysConsumed;
		}
        public override int ConsumeItem(int type, int numRequired)
        {
			if (type == ItemID.ChlorophyteOre) return 0;
            return base.ConsumeItem(type, numRequired);
        }
        public static void AddChlorophyteCraft()
        {
			ChlorophyteAcceleration cRecipe = new ChlorophyteAcceleration(ModContent.GetInstance<sundialAccelerator>());
			cRecipe.AddIngredient(ItemID.ChlorophyteOre, 1);
			cRecipe.AddIngredient(ItemID.MudBlock, ModContent.GetInstance<ConfigServer>().ChlorophyteAmountCraft - 1);
			cRecipe.AddTile(TileID.Sundial);
			cRecipe.SetResult(ItemID.ChlorophyteOre, ModContent.GetInstance<ConfigServer>().ChlorophyteAmountCraft - 1);
			cRecipe.AddRecipe();
		}
	}

	public class NatRecipe : AcceleratedRecipe
	{
		readonly int CreateItemID;
		public NatRecipe(Mod mod, int resultID) : base(mod) { CreateItemID = resultID; }

		public override bool RecipeAvailable()
		{
			return (Main.sundialCooldown <= ModContent.GetInstance<ConfigServer>().MaxBanking) && IsCorrectZone();
		}
		bool IsCorrectZone()
        {
            switch (CreateItemID)
            {
				case ItemID.Coral:
				case ItemID.Starfish:
				case ItemID.Seashell:
					return Main.player[Main.myPlayer].ZoneBeach;
				case ItemID.CrystalShard:
					return Main.player[Main.myPlayer].ZoneHoly && Main.player[Main.myPlayer].ZoneRockLayerHeight;
				default:
					return true;
            }
        }
		public static void AddNaturalRecipe(int tileID, int resultItemID, int amt, int ingredientID = int.MinValue, bool needsWater = false)
        {
			NatRecipe recipe = new NatRecipe(ModContent.GetInstance<sundialAccelerator>(), resultItemID);
			recipe.AddTile(TileID.Sundial);
			recipe.AddTile(tileID);
			recipe.SetResult(resultItemID, amt);
			if (ingredientID != int.MinValue) recipe.AddIngredient(ingredientID);
			recipe.needWater = needsWater;
			recipe.AddRecipe();
        }

		static bool IsHalloween()
        {
			if (Main.halloween) return true;
            else
            {
				DateTime now = DateTime.Now;
				int day = now.Day;
				int month = now.Month;
				if (day >= 20 && month == 10)
				{
					return true;
				}
				else if (day <= 1 && month == 11)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
        }

		public static void AddNaturalRecipes()
        {
			AddNaturalRecipe(TileID.Grass, ItemID.Pumpkin, 8, IsHalloween() ? int.MinValue : ItemID.PumpkinSeed);
			AddNaturalRecipe(TileID.HallowedGrass, ItemID.Pumpkin, 8, IsHalloween() ? int.MinValue : ItemID.PumpkinSeed);
			AddNaturalRecipe(TileID.Sand, ItemID.Coral, ModContent.GetInstance<ConfigServer>().AmountHerbsOnCraft, int.MinValue, true);
			AddNaturalRecipe(TileID.Sand, ItemID.Starfish, 6, int.MinValue, true);
			AddNaturalRecipe(TileID.Sand, ItemID.Seashell, 6, int.MinValue, true);
			AddNaturalRecipe(TileID.Pearlstone, ItemID.CrystalShard, ModContent.GetInstance<ConfigServer>().AmountHerbsOnCraft);
			AddNaturalRecipe(TileID.HallowedIce, ItemID.CrystalShard, ModContent.GetInstance<ConfigServer>().AmountHerbsOnCraft);
		}
	}

	public class TreeRecipe : AcceleratedRecipe
	{
		public TreeRecipe(Mod mod) : base(mod) {}

		public override bool RecipeAvailable()
		{
			return (Main.sundialCooldown <= ModContent.GetInstance<ConfigServer>().MaxBanking) && Main.player[Main.myPlayer].ZoneOverworldHeight;
		}
		public static void AddTreeCraft(int tileID, int resultItemID)
        {
			TreeRecipe recipe = new TreeRecipe(ModContent.GetInstance<sundialAccelerator>());
			recipe.AddIngredient(ItemID.Acorn);
			recipe.AddTile(TileID.Sundial);
			recipe.AddTile(tileID);
			recipe.SetResult(resultItemID, ModContent.GetInstance<ConfigServer>().AmountWoodOnCraft);
			recipe.AddRecipe();
        }
		public static void AddTreeRecipes()
        {
			AddTreeCraft(TileID.Grass, ItemID.Wood);
			AddTreeCraft(TileID.SnowBlock, ItemID.BorealWood);
			AddTreeCraft(TileID.Sand, ItemID.PalmWood);
			AddTreeCraft(TileID.JungleGrass, ItemID.RichMahogany);
			AddTreeCraft(TileID.FleshGrass, ItemID.Shadewood);
			AddTreeCraft(TileID.CorruptGrass, ItemID.Ebonwood);
			AddTreeCraft(TileID.HallowedGrass, ItemID.Pearlwood);
		}
	}

	public class HerbRecipe : AcceleratedRecipe
	{
		readonly int CreateItemID;
		public HerbRecipe(Mod mod, int resultItemID) : base(mod) { CreateItemID = resultItemID; }

		public override bool RecipeAvailable(){
			return IsHerbBlooming() && (Main.sundialCooldown <= ModContent.GetInstance<ConfigServer>().MaxBanking);
		}
		public bool IsHerbBlooming()
        {
			switch (CreateItemID)
			{
				//blinkroot and shiverthorn omitted as they don't have specific bloom conditions
				//conditions found in Terraria.WorldGen.KillTile part to give seeds
				case ItemID.Blinkroot:
					return Main.player[Main.myPlayer].ZoneDirtLayerHeight || Main.player[Main.myPlayer].ZoneRockLayerHeight;
				case ItemID.Daybloom:
					return Main.dayTime;
				case ItemID.Moonglow:
					return !Main.dayTime;
				case ItemID.Deathweed:
					return (!Main.dayTime && (Main.bloodMoon || Main.moonPhase == 0));
				case ItemID.Waterleaf:
					return (Main.raining || Main.cloudAlpha > 0f);
				case ItemID.Fireblossom:
					return (!Main.raining && Main.dayTime && Main.time > 40500.0);
				case ItemID.Cactus:
					return !Main.player[Main.myPlayer].ZoneBeach;
				case ItemID.Mushroom:
					return Main.player[Main.myPlayer].ZoneOverworldHeight;
				case ItemID.GlowingMushroom:
					return Main.player[Main.myPlayer].ZoneGlowshroom;
				default:
					return true;
			}
		}
		public static void AddHerbRecipe(int tileID, int resultID)
        {
			HerbRecipe recipe = new HerbRecipe(ModContent.GetInstance<sundialAccelerator>(), resultID);
			recipe.AddTile(TileID.Sundial);
			recipe.AddTile(tileID);
			recipe.SetResult(resultID, ModContent.GetInstance<ConfigServer>().AmountHerbsOnCraft);
			recipe.AddRecipe();
        }
		public static void AddHerbRecipe(int[] tileIDs, int resultID)
        {
			foreach (int tileID in tileIDs) AddHerbRecipe(tileID, resultID);
        }
		public static void AddHerbRecipes()
        {
			AddHerbRecipe(TileID.Grass, ItemID.Daybloom);
			AddHerbRecipe(TileID.HallowedGrass, ItemID.Daybloom);
			AddHerbRecipe(TileID.JungleGrass, ItemID.Moonglow);
			AddHerbRecipe(TileID.Dirt, ItemID.Blinkroot);
			int[] DeathweedTiles = new int[] { TileID.CorruptGrass, TileID.Ebonstone, TileID.FleshGrass, TileID.Crimstone };
			AddHerbRecipe(DeathweedTiles, ItemID.Deathweed);
            int[] WaterleafTiles = new int[] { TileID.Sand, TileID.Pearlsand };
			AddHerbRecipe(WaterleafTiles, ItemID.Waterleaf);
			AddHerbRecipe(TileID.Ash, ItemID.Fireblossom);
			int[] ShiverthornTiles = new int[] { TileID.SnowBlock, TileID.IceBlock, TileID.CorruptIce, TileID.FleshIce, TileID.HallowedIce };
			AddHerbRecipe(ShiverthornTiles, ItemID.Shiverthorn);
			AddHerbRecipe(TileID.Sand, ItemID.Cactus);
			AddHerbRecipe(TileID.MushroomGrass, ItemID.GlowingMushroom);
			AddHerbRecipe(TileID.Grass, ItemID.Mushroom);
		}
	}

	public class sundialAccelerator : Mod
	{
		
		public override void Load()
		{
			Logger.InfoFormat("{0} example logging", Name);
		}
        public override void PostSetupContent()
        {
            
        }
        public override void Unload() 
		{

		}

		public override void AddRecipes()
        {
			if (ModContent.GetInstance<ConfigServer>().HerbRecipesEnabled) HerbRecipe.AddHerbRecipes();
			if (ModContent.GetInstance<ConfigServer>().ChlorophyteEnabled) ChlorophyteAcceleration.AddChlorophyteCraft();
			if (ModContent.GetInstance<ConfigServer>().TreeRecipesEnabled) TreeRecipe.AddTreeRecipes();
			if (ModContent.GetInstance<ConfigServer>().NatRecipesEnabled) NatRecipe.AddNaturalRecipes();
		}
	}
}