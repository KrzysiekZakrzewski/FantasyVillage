using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs
{
    public class InputManager : MonoBehaviour
    {
        private static Dictionary<int, PlayerInput> playersLUT = new();

        private static PlayerInput CreatePlayer()
        {
            PlayerInput newPlayer = new();

            playersLUT.Add(playersLUT.Count, newPlayer);

            return newPlayer;
        }

        public static PlayerInput GetPlayer(int id)
        {
            var result = playersLUT.TryGetValue(id, out PlayerInput player);

            if(result)
                return player;

            return CreatePlayer();
        }
    }
}