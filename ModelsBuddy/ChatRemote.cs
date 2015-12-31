﻿using EloBuddy;
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
        public static void SetModelFor(AIHeroClient target, string model)
        {
            if (target.Model != model)
            {
                target.SetModel(model);
                BroadcastSetModel(target, model);
            }
        }

        private static void BroadcastSetModel(AIHeroClient target, string model)
        {
            Chat.Say("setmodel-{0}-{1}", model, target.Name);
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
