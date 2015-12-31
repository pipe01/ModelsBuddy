using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.Networking;

namespace ModelsBuddy
{
    class Program
    {
        static Menu Menu, SkinMenu;

        static CommandManager CMD;

        static void Main(string[] args)
        {
            CMD = new CommandManager();
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
            Drawing.OnDraw += Drawing_OnDraw;
            Chat.OnInput += Chat_OnInput;

            CMD.RegisterCommand("setskinid", a => 
            {
                if (a.Length != 1) return false;

                int skinid = -1;
                if (!int.TryParse(a[0], out skinid)) return false;
                Player.SetSkinId(skinid);
                Chat.Print("Set current skin id to {0}", System.Drawing.Color.LightGreen, skinid.ToString());
                return true;
            });

            CMD.RegisterCommand("setmodel", a =>
            {
                if (a.Length != 1) return false;

                ChatRemote.SetModelFor(Player.Instance, a[0]);
                Chat.Print("Set current model to {0}", System.Drawing.Color.LightGreen, a[0]);

                return true;
            });

            Chat.OnMessage += Chat_OnMessage;

            Chat.Print("ModelsBuddy v1.0.2 loaded succesfully!",
                System.Drawing.Color.LightGreen);
        }

        private static void Chat_OnMessage(AIHeroClient sender, ChatMessageEventArgs args)
        {
            string[] msg = args.Message.Split('-');
            if (msg[0] == "setmodel")
            {
                AIHeroClient target = ChatRemote.GetHeroFromName(msg[2]);
                target.SetModel(msg[1]);
            }               
        }

        private static void Chat_OnInput(ChatInputEventArgs args)
        {
            if (args.Input[0] == '.')
            {
                string[] cmd = args.Input.Split(' ');
                if (CMD.ExecuteCommand(cmd[0].Substring(1, cmd[0].Length - 1), cmd.Skip(1).ToArray()))
                    args.Process = false;
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            string time = DateTime.Now.ToShortTimeString();
            Drawing.DrawText(0, 500, System.Drawing.Color.LightGreen, time);
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Menu = MainMenu.AddMenu("Addon1", "addon1");
            Menu.AddGroupLabel("Addon1");

            SkinMenu = Menu.AddSubMenu("Skin", "skinChoose");
            SkinMenu.AddGroupLabel("Choose your skin");

            System.Windows.Forms.TrackBar skinIdBar = new System.Windows.Forms.TrackBar();
            skinIdBar.Maximum = 10;
            skinIdBar.Minimum = 0;
            skinIdBar.Value = Player.Instance.SkinId;
            //SkinMenu.Add("skinIdChooser", skinIdBar);
        }
    }
}
