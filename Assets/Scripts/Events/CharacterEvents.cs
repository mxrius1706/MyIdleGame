using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

  
    public static class CharacterEvents
    {
        public static UnityAction<int, GameObject> characterDamaged;
        public static UnityAction<int, GameObject> characterHealed;
    }
