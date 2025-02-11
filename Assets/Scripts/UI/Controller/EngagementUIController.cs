using UnityEngine;
using ViewSystem.Implementation;

namespace Engagement.UI
{
    public class EngagementUIController : SingleViewTypeStackController
    {
        [SerializeField]
        private EngagementView engagementView;

        public EngagementView GetEngagementView()
        {
            return engagementView;
        }
    }
}