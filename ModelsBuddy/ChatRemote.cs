using EloBuddy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Chat.Say("set model {0} to {1}", model, target.Name);
        }
    }
}
