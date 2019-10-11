using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Api.Clients.Databases
{
    public class TestCardDatabase : MonoBehaviour
    {
        public static Dictionary<int, Dictionary<string, object>> Cards
            = new Dictionary<int, Dictionary<string, object>>
            {
                { 0, null },
                {
                    1, new Dictionary<string, object>
                    {
                        { "@CardValue", 1 },
                        { "@CardText", "Atomic" },
                    }
                },
                {
                    2, new Dictionary<string, object>
                    {
                        { "@CardValue", 2 },
                        { "@CardText", "Cyber" },
                    }
                },
                {
                    3, new Dictionary<string, object>
                    {
                        { "@CardValue", 3 },
                        { "@CardText", "Dark" },
                    }
                },
                {
                    4, new Dictionary<string, object>
                    {
                        { "@CardValue", 4 },
                        { "@CardText", "Electric" },
                    }
                },
                {
                    5, new Dictionary<string, object>
                    {
                        { "@CardValue", 5 },
                        { "@CardText", "Fire" },
                    }
                },
                {
                    6, new Dictionary<string, object>
                    {
                        { "@CardValue", 6 },
                        { "@CardText", "Leaf" },
                    }
                },
                {
                    7, new Dictionary<string, object>
                    {
                        { "@CardValue", 7 },
                        { "@CardText", "Metal" },
                    }
                },
                {
                    8, new Dictionary<string, object>
                    {
                        { "@CardValue", 8 },
                        { "@CardText", "Water" },
                    }
                }
            };
    }
}
