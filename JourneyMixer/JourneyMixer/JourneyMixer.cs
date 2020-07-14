using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Bestiary;
using TerrariaApi.Server;

namespace JourneyMixer
{
    [ApiVersion(2, 1)]
    public class JourneyMixer : TerrariaPlugin
    {
        public override Version Version => new Version("1.0");

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
                if (bitsbyte[3])
                {
                    TShockAPI.TShock.Log.ConsoleInfo("Fixing journeymode for ix {0}", args.Msg.whoAmI);
                    bitsbyte[3] = false;
                    args.Msg.readBuffer[args.Length] = bitsbyte;
                }
            }
        }
    }
}
