using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace sundialAccelerator
{

	public class acceleratedRecipe : ModRecipe
    {
        public acceleratedRecipe(Mod mod) : base(mod) {}

        public override void OnCraft(Item item)
        {
            Main.sundialCooldown++;
        }
    }

	public class chlorophyteAcceleration : acceleratedRecipe
	{
		public chlorophyteAcceleration(Mod mod) : base(mod) {}

		public override bool RecipeAvailable()
		{
			if (!ModContent.GetInstance<ConfigServer>().chlorophyteEnabled) return false;
			return (Main.sundialCooldown <= ModContent.GetInstance<ConfigServer>().maxBanking);
		}

		public override void OnCraft(Item item)
		{
			Main.sundialCooldown += ModContent.GetInstance<ConfigServer>().chlorophyteDaysConsumed;
		}
	}

	public class natRecipe : acceleratedRecipe
	{
		public natRecipe(Mod mod) : base(mod){}

		public override bool RecipeAvailable()
		{
			return (Main.sundialCooldown <= ModContent.GetInstance<ConfigServer>().maxBanking) && ModContent.GetInstance<ConfigServer>().natRecipesEnabled;
		}
	}

	public class treeRecipe : acceleratedRecipe
	{
		public treeRecipe(Mod mod) : base(mod) {}

		public override bool RecipeAvailable()
		{
			if (!ModContent.GetInstance<ConfigServer>().treeRecipesEnabled) return false;
			return (Main.sundialCooldown <= ModContent.GetInstance<ConfigServer>().maxBanking);
		}
	}

	public class herbRecipe : acceleratedRecipe
	{
		string herb;
		public herbRecipe(Mod mod, string herbName) : base(mod)
		{
			herb = herbName;
		}

		public override bool RecipeAvailable(){
			if (!ModContent.GetInstance<ConfigServer>().herbRecipesEnabled) return false;
			if (Main.sundialCooldown > ModContent.GetInstance<ConfigServer>().maxBanking) return false;
			switch(herb) 
			{
				//blinkroot and shiverthorn omitted as they don't have specific bloom conditions
				//conditions found in Terraria.WorldGen.KillTile part to give seeds
				case "daybloom":
					return Main.dayTime;
				case "moonglow":
					return !Main.dayTime;
				case "deathweed":
					return (!Main.dayTime && (Main.bloodMoon || Main.moonPhase == 0));
				case "waterleaf":
					return (Main.raining || Main.cloudAlpha > 0f);
				case "fireblossom":
					return (!Main.raining && Main.dayTime && Main.time > 40500.0);
				case "beach":
					return Main.player[Main.myPlayer].ZoneBeach;
				case "cactus":
					return !Main.player[Main.myPlayer].ZoneBeach;
				default:
					return true;
			}
		}
	}

	public class sundialAccelerator : Mod
	{
		
		public override void Load()
		{
			Logger.InfoFormat("{0} example logging", Name);
		}

		public override void Unload() 
		{

		}

		public override void AddRecipes()
        {
			herbRecipe hRecipe = new herbRecipe(this, "daybloom");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Grass);
			hRecipe.SetResult(ItemID.Daybloom, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "daybloom");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.HallowedGrass);
			hRecipe.SetResult(ItemID.Daybloom, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "moonglow");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.JungleGrass);
			hRecipe.SetResult(ItemID.Moonglow, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "blinkroot");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Dirt);
			hRecipe.SetResult(ItemID.Blinkroot, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "blinkroot");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Mud);
			hRecipe.SetResult(ItemID.Blinkroot, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "deathweed");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.CorruptGrass);
			hRecipe.SetResult(ItemID.Deathweed, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "deathweed");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.FleshGrass);
			hRecipe.SetResult(ItemID.Deathweed, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "waterleaf");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Sand);
			hRecipe.SetResult(ItemID.Waterleaf, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "waterleaf");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Pearlsand);
			hRecipe.SetResult(ItemID.Waterleaf, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "fireblossom");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Ash);
			hRecipe.SetResult(ItemID.Fireblossom, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "shiverthorn");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.SnowBlock);
			hRecipe.SetResult(ItemID.Shiverthorn, 15);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "beach");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Sand);
			hRecipe.SetResult(ItemID.Seashell, 16);
			hRecipe.AddRecipe();

			hRecipe = new herbRecipe(this, "beach");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Sand);
			hRecipe.SetResult(ItemID.Starfish, 16);
			hRecipe.AddRecipe();
			
			hRecipe = new herbRecipe(this, "cactus");
			hRecipe.AddTile(TileID.Sundial);
			hRecipe.AddTile(TileID.Sand);
			hRecipe.SetResult(ItemID.Cactus, 16);
			hRecipe.AddRecipe();

            chlorophyteAcceleration cRecipe = new chlorophyteAcceleration(this);
            cRecipe.AddIngredient(ItemID.ChlorophyteOre, 1);
			cRecipe.AddIngredient(ItemID.MudBlock, 17);
            cRecipe.AddTile(TileID.Sundial);
            cRecipe.SetResult(ItemID.ChlorophyteOre, 18);
            cRecipe.AddRecipe();

			treeRecipe woodRecipe = new treeRecipe(this);
			woodRecipe.AddIngredient(ItemID.Acorn, 1);
			woodRecipe.AddTile(TileID.Sundial);
			woodRecipe.AddTile(TileID.Grass);
			woodRecipe.SetResult(ItemID.Wood, 16);
			woodRecipe.AddRecipe();

			woodRecipe = new treeRecipe(this);
			woodRecipe.AddIngredient(ItemID.Acorn, 1);
			woodRecipe.AddTile(TileID.Sundial);
			woodRecipe.AddTile(TileID.SnowBlock);
			woodRecipe.SetResult(ItemID.BorealWood, 16);
			woodRecipe.AddRecipe();

			woodRecipe = new treeRecipe(this);
			woodRecipe.AddIngredient(ItemID.Acorn, 1);
			woodRecipe.AddTile(TileID.Sundial);
			woodRecipe.AddTile(TileID.Sand);
			woodRecipe.SetResult(ItemID.PalmWood, 16);
			woodRecipe.AddRecipe();

			woodRecipe = new treeRecipe(this);
			woodRecipe.AddIngredient(ItemID.Acorn, 1);
			woodRecipe.AddTile(TileID.Sundial);
			woodRecipe.AddTile(TileID.JungleGrass);
			woodRecipe.SetResult(ItemID.RichMahogany, 16);
			woodRecipe.AddRecipe();

			woodRecipe = new treeRecipe(this);
			woodRecipe.AddIngredient(ItemID.Acorn, 1);
			woodRecipe.AddTile(TileID.Sundial);
			woodRecipe.AddTile(TileID.FleshGrass);
			woodRecipe.SetResult(ItemID.Shadewood, 16);
			woodRecipe.AddRecipe();

			woodRecipe = new treeRecipe(this);
			woodRecipe.AddIngredient(ItemID.Acorn, 1);
			woodRecipe.AddTile(TileID.Sundial);
			woodRecipe.AddTile(TileID.CorruptGrass);
			woodRecipe.SetResult(ItemID.Ebonwood, 16);
			woodRecipe.AddRecipe();

			woodRecipe = new treeRecipe(this);
			woodRecipe.AddIngredient(ItemID.Acorn, 1);
			woodRecipe.AddTile(TileID.Sundial);
			woodRecipe.AddTile(TileID.HallowedGrass);
			woodRecipe.SetResult(ItemID.Pearlwood, 16);
			woodRecipe.AddRecipe();

			natRecipe recipe = new natRecipe(this);
			recipe.AddTile(TileID.Sundial);
			recipe.AddTile(TileID.MushroomGrass);
			recipe.SetResult(ItemID.GlowingMushroom, 16);
			recipe.AddRecipe();

			recipe = new natRecipe(this);
			recipe.AddIngredient(ItemID.PumpkinSeed, 1);
			recipe.AddTile(TileID.Sundial);
			recipe.SetResult(ItemID.Pumpkin, 8);
			recipe.AddRecipe();

			recipe = new natRecipe(this);
			recipe.AddTile(TileID.Sundial);
			recipe.AddTile(TileID.Sand);
			recipe.needWater = true;
			recipe.SetResult(ItemID.Coral, 16);
			recipe.AddRecipe();

        }
	}
}