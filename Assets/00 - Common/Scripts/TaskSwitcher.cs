using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class TaskSwitcher : MonoBehaviour
    {
        [SerializeField] List<GameObject> taskHolders;

        void Start()
        {
            SwitchToTask(taskHolders[0]);
        }

        public void SwitchToTask(GameObject taskHolder)
        {
            foreach (var holder in taskHolders)
            {
                holder.SetActive(false);
            }
            taskHolder.SetActive(true);
        }
    }
}