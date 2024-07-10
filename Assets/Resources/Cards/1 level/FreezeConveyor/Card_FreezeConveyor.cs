using System;
using Features.Conveyor.Scripts;
using UnityEngine;

namespace Resources.Cards._1_level.FreezeConveyor
{
    public class CardFreezeConveyor : MonoBehaviour
    {
        public Conveyor conveyorScript;

        void Start()
        {
            conveyorScript = FindObjectOfType<Conveyor>();
        }

        //Замораживает(останавливает) конвейер на 5 секунд
        private void OnMouseDown()
        {
            if (!conveyorScript.IsInvoking(nameof(Conveyor.DisableConveyor)))
            {
                conveyorScript.DisableConveyor();
                conveyorScript.Invoke(nameof(conveyorScript.EnableConveyor), 5f);
            }
        }
    }
}
