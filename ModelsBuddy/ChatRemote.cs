using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModelsBuddy
{
    class ChatRemote
    {
        private static Dictionary<string, string> DefaultModels = new Dictionary<string, string>();
        private static Dictionary<string, string> DefaultSkins = new Dictionary<string, string>();

        public static void Init()
        {
            foreach (AIHeroClient item in EntityManager.Heroes.Allies)
            {
                DefaultModels.Add(item.Name, item.Model);
                DefaultSkins.Add(item.Name, item.SkinId.ToString());
            }
        }

        public static void SetModelFor(AIHeroClient target, string model)
        {
            if (target.Model != model)
            {
                target.SetModel(model);
                BroadcastSetModel(target, model);
            }
        }

        public static void SetSkinIdFor(AIHeroClient target, int id)
        {
            if (target.SkinId != id)
            {
                target.SetSkinId(id);
                BroadcastSetSkinID(target, id);
            }
        }

        public static void ResetModelFor(AIHeroClient target, bool broadcast = true)
        {
            if (target.Team == Player.Instance.Team)
            {
                target.SetModel(DefaultModels[target.Name]);
                if (broadcast) BroadcastResetModel(target);
            }
        }

        private static void BroadcastSetModel(AIHeroClient target, string model)
        {
            Chat.Say("setmodel-{0}-{1}", model, target.Name);
        }

        private static void BroadcastSetSkinID(AIHeroClient target, int id)
        {
            Chat.Say("setskinid-{0}-{1}", id.ToString(), target.Name);
        }

        private static void BroadcastResetModel(AIHeroClient target)
        {
            Chat.Say("resetmodel-{0}", target.Name);
        }

        public static AIHeroClient GetHeroFromName(string name)
        {
            foreach (AIHeroClient item in EntityManager.Heroes.Allies)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }
    }
}
