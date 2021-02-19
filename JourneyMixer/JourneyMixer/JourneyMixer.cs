using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;

namespace JourneyMixer
{
    [ApiVersion(2, 1)]
    public class JourneyMixer : TerrariaPlugin
    {
        public override Version Version => new Version("1.1");

        public override string Name => "JourneyMixer";

        public override string Author => "rustly";

        public override string Description => "i hate cy";

        public JourneyMixer(Main game) : base(game)
        {

        }

        public override void Initialize()
        {
            ServerApi.Hooks.NetGetData.Register(this, OnData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.NetGetData.Deregister(this, OnData);
            }
            base.Dispose(disposing);
        }

        private void OnData(GetDataEventArgs args)
        {
            if (args.MsgID == PacketTypes.PlayerInfo)
            {
                var bitsbyte = (BitsByte)args.Msg.readBuffer[args.Length];

                if ((Main.GameMode == 3 && !bitsbyte[3]) || (Main.GameMode != 3 && bitsbyte[3]))
                {
                    bitsbyte[3] = !bitsbyte[3];
                    args.Msg.readBuffer[args.Length] = bitsbyte;
                    TShockAPI.TShock.Log.ConsoleInfo("[JM] {0} journeymode for index {1}.", bitsbyte[3] ? "Enabled" : "Disabled", args.Msg.whoAmI);
                }
            }
        }
    }
}
