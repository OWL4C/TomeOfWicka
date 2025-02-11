using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
// Using StardewUI for Ingame UI
using StardewUI.Framework;


namespace TomeOfWicka
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {

        // Initialized in GameLaunched
        private string viewAssetPrefix = null!;
        private IViewEngine viewEngine = null!;
        



        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.GameLaunched += this.GameLaunched;
            viewAssetPrefix = $"Mods/{ModManifest.UniqueID}/Views";


        }

        // Setup ViewEngine on Launch
        private void GameLaunched(object? _sender, GameLaunchedEventArgs _e){
            viewEngine = Helper.ModRegistry.GetApi<IViewEngine>("focustense.StardewUI")!;
            viewEngine.RegisterSprites($"Mods/{ModManifest.UniqueID}/Sprites", "assets/sprites");
            viewEngine.RegisterViews(viewAssetPrefix, "assets/views");
            viewEngine.EnableHotReloadingWithSourceSync();
            viewEngine.PreloadAssets();
            this.Monitor.Log($"{viewAssetPrefix}");
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object? sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;
            if (e.Button == SButton.L)
               ShowConfig();

            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }


        
    }
}
